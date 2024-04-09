using ErrorOr;
using MediatR;

using Sales.Application.Interfaces;
using Sales.Application.Interfaces.Persistence;
using Sales.Domain.CategoryAggregate.ValueObjects;
using Sales.Domain.ProductAggregate.Entities;

namespace Sales.Application.Commands.AddDiscount;

public class AddDiscountCommandHandler : IRequestHandler<AddProductDiscountCommand, ErrorOr<ProductDiscount>>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDomainEventPublisher _domainEventPublisher;
    public AddDiscountCommandHandler(IProductRepository productRepository,
                                    IUnitOfWork unitOfWork,
                                    IDomainEventPublisher domainEventPublisher)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _domainEventPublisher = domainEventPublisher;
    }

    public async Task<ErrorOr<ProductDiscount>> Handle(AddProductDiscountCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);

        if (product is null)
        {
            return Error.NotFound($"Product with id {request.ProductId} not found");
        }

        var productDiscount = ProductDiscount.Create(
            request.DiscountPercentage,
            request.StartDate,
            request.EndDate
        );

        var result = product.AddProductDiscount(request.CategoryId, productDiscount);

        if (result.IsError)
        {
             return Error.NotFound($"Discount is not added to the product.");
        }

        await _unitOfWork.AddOperation(_productRepository.UpdateProductAsync(product, cancellationToken));

        await _unitOfWork.AddOperation(_domainEventPublisher.SaveProductEventsAsync(product, cancellationToken));

        await _unitOfWork.CommitChanges(cancellationToken);

        return productDiscount;
    }
}
