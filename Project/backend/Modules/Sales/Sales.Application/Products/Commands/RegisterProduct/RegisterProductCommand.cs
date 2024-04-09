using ErrorOr;
using MediatR;
using Sales.Domain.ProductAggregate;

namespace Sales.Application.Commands.RegisterProduct;

public record RegisterProductCommand(
    string ProductName,
    decimal? UnitPrice, 
    string Description,
    Guid CategoryId,
    List<ProductDiscountCommand> ProductDiscounts, 
    List<ImageCommand> Images,
    List<InventoryCommand> Inventories,
    List<StatusCommand> StatusHistories
    ) : IRequest<ErrorOr<Product>>;

public record ProductDiscountCommand(
    decimal DiscountPercentage,
    DateTime StartDate,
    DateTime EndDate,
    Guid? CategoryId
    );
public record ImageCommand(
    string Url, 
    string Alt,
    string Title,
    string ContentType
    );

public record InventoryCommand(
    decimal UnitsInStock,
    decimal UnitsOnOrder,
    decimal ReorderLevel,
    string Location
    );

public record StatusCommand(
    string ProductStatus,
    string Description
    );
