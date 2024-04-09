using ErrorOr;
using MediatR;
using Sales.Application.Interfaces;
using Sales.Application.Interfaces.Persistence;
using Sales.Domain.CategoryAggregate.ValueObjects;
using Sales.Domain.ProductAggregate;
using Sales.Domain.ProductAggregate.Entities;
using Sales.Domain.ProductAggregate.Enums;


namespace Sales.Application.Commands.RegisterProduct;

public class CreateProductCommandHandler : 
    IRequestHandler<RegisterProductCommand, ErrorOr<Product>>
{
    private readonly IProductRepository _productRepository;
    private readonly IDomainEventPublisher _domainEventPublisher;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductCommandHandler(IProductRepository productRepository,
         IUnitOfWork unitOfWork, IDomainEventPublisher domainEventPublisher)
    {
        _productRepository = productRepository;
        _domainEventPublisher = domainEventPublisher;
        _unitOfWork = unitOfWork;
    }
    public async Task<ErrorOr<Product>> Handle(RegisterProductCommand request, CancellationToken cancellationToken)
    {
        // Register product
        await Task.CompletedTask;
        var product = Product.Create(
        productName: request.ProductName,
        unitPrice: request.UnitPrice, 
        description: request.Description,
        categoryId: CategoryId.Create(request.CategoryId),
    
        images: request.Images.ConvertAll(image => ProductImage.Create(
            image.Url, 
            image.Alt, 
            image.Title, 
            image.ContentType)),
        inventories: request.Inventories.ConvertAll(inventory => ProductInventory.Create(
            inventory.UnitsInStock,
            inventory.UnitsOnOrder,
            inventory.ReorderLevel,
            inventory.Location)),
        statusHistories: request.StatusHistories.ConvertAll(statusDto => ProductStatus.Create(
        status: Enum.TryParse<Status>(statusDto.ProductStatus, out var parsedStatus) ? parsedStatus : default,
        description: statusDto.Description
        ))
        );

        // Persist product
        await _unitOfWork.AddOperation(_productRepository.AddProductAsync(product));

        await _unitOfWork.AddOperation( _domainEventPublisher.SaveProductEventsAsync(product, cancellationToken));

        await _unitOfWork.CommitChanges(cancellationToken);

        // Return product
        return product;
    }
}
