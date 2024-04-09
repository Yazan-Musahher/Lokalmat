using ErrorOr;
using MediatR;
using Sales.Application.interfaces;
using Sales.Application.Interfaces;
using Sales.Application.Queries.GetProductDetails;
using Sales.Domain.OrderAggregate;
using Sales.Domain.OrderAggregate.Entities;
using Sales.Domain.OrderAggregate.ValueObjects;
namespace Sales.Application.Commands.CreateOrder;

public class CreateProductCommandHandler : 
    IRequestHandler<CreateOrderCommand, ErrorOr<OrderAggregate>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IDomainEventPublisher _domainEventPublisher;
    public readonly IMediator _mediator;

    private readonly IUnitOfWork _unitOfWork;

    public CreateProductCommandHandler(
        IOrderRepository orderRepository,
        IDomainEventPublisher domainEventPublisher,
        IMediator mediator,
        IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _domainEventPublisher = domainEventPublisher;
        _mediator = mediator;
        _unitOfWork = unitOfWork;
    }
   public async Task<ErrorOr<OrderAggregate>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {   
        var orderItems = new List<OrderItem>();

        foreach (var item in request.OrderItems)
        {
            var query = new GetProductDetailsQuery(item.productId);
            var result = await _mediator.Send(query, cancellationToken);

            if (result.IsError)
            {
                return Error.NotFound($"Product with ID {item.productId} not found.");
            }

            var productDto = result.Value;
            orderItems.Add(OrderItem.Create(
                productId: productDto.Id,
                productDto.ProductName,
                item.quantity,
                productDto.UnitPrice ?? 0,
                item.description));
        }


        var order = OrderAggregate.Create(
            customerId: CustomerId.Create(request.CustomerId),
            shippingAddress: ShippingAddress.Create(
                request.ShippingAddress.StreetName,
                request.ShippingAddress.StreetNumber,
                request.ShippingAddress.PostalCode,
                request.ShippingAddress.City, 
                request.ShippingAddress.AdditionalDetails),
            billingAddress: BillingAddress.Create(
                request.BillingAddress.Name,
                request.BillingAddress.StreetName,
                request.BillingAddress.StreetNumber,
                request.BillingAddress.PostalCode,
                request.BillingAddress.City,
                request.BillingAddress.AdditionalDetails),
            orderItems: orderItems,
            shippingMethod: request.ShippingMethod
            
            );

        await _unitOfWork.AddOperation(_orderRepository.AddOrderAsync(order, cancellationToken));

        await _unitOfWork.AddOperation( _domainEventPublisher.SaveEventsAsync(order, cancellationToken));

        await _unitOfWork.CommitChanges(cancellationToken);

        return order;
    }

}