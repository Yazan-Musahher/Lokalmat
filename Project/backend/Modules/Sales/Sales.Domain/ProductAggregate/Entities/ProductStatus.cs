using Sales.Domain.Models;
using Sales.Domain.ProductAggregate.Enums;
using Sales.Domain.ProductAggregate.ValueObjects;

namespace Sales.Domain.ProductAggregate.Entities;

public sealed class ProductStatus : Entity<ProductStatusId>
{
    public Status Status { get; private set; }
    public string Description { get; private set; }

    private ProductStatus(ProductStatusId statusId, Status status, string description) :
        base(statusId)
    {
        if (status == Status.Inactive && string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentException("Description is required when changing status to inactive");
        }
        Status = status;
        Description = description;
    }

    public static ProductStatus Create(
        Status status, 
        string description)
    {
        return new ProductStatus(
            ProductStatusId.CreateUnique(),
            status,
            description);
    }

    public static ProductStatus Create(
        ProductStatusId statusId, 
        Status status, 
        string description)
    {
        return new ProductStatus(
            statusId,
            status,
            description);
    }

    public void ChangeStatus(Status status, string description)
    {
        if (status == Status.Inactive && string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentException("Description is required when changing status to inactive");
        }
        
        Status = status;
        Description = description;
    }


}
