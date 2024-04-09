using ErrorOr;
using Sales.Domain.CategoryAggregate.ValueObjects;
using Sales.Domain.Models;
using Sales.Domain.ProductAggregate.Entities;
using Sales.Domain.ProductAggregate.Events;
using Sales.Domain.ProductAggregate.ValueObjects;

namespace Sales.Domain.ProductAggregate;

public sealed class Product : AggregateRoot<ProductId>
{
    private readonly List<ProductImage> _images = new();
    private readonly List<ProductStatus> _statusHistories  = new();
    private readonly List<ProductDiscount> _productDiscounts = new();
    private readonly List<ProductInventory> _inventories = new();
    
    public IReadOnlyCollection<ProductImage> Images => _images.AsReadOnly();
    public IReadOnlyCollection<ProductDiscount> ProductDiscounts => _productDiscounts.AsReadOnly();
    public IReadOnlyCollection<ProductInventory> Inventories => _inventories.AsReadOnly();
    public IReadOnlyCollection<ProductStatus> StatusHistories => _statusHistories.AsReadOnly();

    public CategoryId CategoryId { get; private set; }

    public string ProductName { get; private set; }
    public decimal? UnitPrice { get; private set; }
    public string Description { get; private set; }
    public DateTime CreatedDateTime { get; private set; }

    private Product(ProductId productId, 
                    string productName,
                    decimal? unitPrice, 
                    string description,
                    CategoryId categoryId,
                    List<ProductImage> images,
                    List<ProductInventory> inventories,
                    List<ProductStatus> statusHistories,
                    DateTime createdDateTime,
                    List<ProductDiscount>? productDiscounts = null) :
        base(productId)
    {
        if (string.IsNullOrWhiteSpace(productName))
        {
            throw new ArgumentException("Product name cannot be empty", nameof(productName));
        }

        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentException("Product description cannot be empty", nameof(description));
        }

        ProductName = productName;
        UnitPrice = unitPrice;
        Description = description;
        CategoryId = categoryId;
        _images = new List<ProductImage>(images);
        _inventories = new List<ProductInventory>(inventories);
        _statusHistories = new List<ProductStatus>(statusHistories);
        CreatedDateTime = createdDateTime;
        _productDiscounts = productDiscounts ?? new List<ProductDiscount>();
    }

    public static Product Create(
        string productName,
        decimal? unitPrice,
        string description,
        CategoryId categoryId,
        List<ProductImage> images,
        List<ProductInventory> inventories,
        List<ProductStatus> statusHistories,
        List<ProductDiscount>? productDiscounts = null
        )
    {
        var product =  new Product(
            ProductId.CreateUnique(),
            productName,
            unitPrice,
            description,
            categoryId,
            images,
            inventories,
            statusHistories,
            DateTime.UtcNow,
            productDiscounts);
        product.RaiseDomainEvent(new ProductRegisteredEvent(product.Id.Value, categoryId.Value, DateTime.UtcNow));
        return product;    
    }

    // constructor for deserialization
    public static Product Create(
        ProductId productId,
        string productName,
        decimal? unitPrice,
        string description,
        CategoryId categoryId,
        List<ProductImage> images,
        List<ProductInventory> inventories,
        List<ProductStatus> statusHistories,
        List<ProductDiscount>? productDiscounts = null
        )
    {
        return new Product(
            productId,
            productName,
            unitPrice,
            description,
            categoryId,
            images,
            inventories,
            statusHistories,
            DateTime.UtcNow,
            productDiscounts
            );
    }

    // Update product details
    public ErrorOr<Success> UpdateProduct(string productName, decimal? unitPrice, string description)
    {
        ProductName = productName;
        UnitPrice = unitPrice;
        Description = description;

        return Result.Success;
    }
    

    // Add status history
    public ErrorOr<Success> AddStatusHistory(ProductStatus status)
    {
        if (status is null)
        {
            return Error.Failure("Status cannot be null");
        }
        _statusHistories.Add(status);
        return Result.Success;
    }



    // Add product image
    public ErrorOr<Success> AddProductImage(ProductImage productImage)
    {
        if (productImage is null)
        {
            return Error.Failure("Product image cannot be null");
        }
        _images.Add(productImage);

        return Result.Success;
    }

    // Update product image
    public ErrorOr<Success> UpdateProductImage(Guid productImageId, string url, string alt, string title, string contentType)
    {
        
        var existingImage = _images.FirstOrDefault(x => x.Id.Value == productImageId);
        

        if (existingImage is null)
        {
            return Error.NotFound($"Image with ID {productImageId} not found");
        }

        existingImage.UpdateDetails(url, alt, title, contentType);
        
        return Result.Success;
    }

    // Delete product image
    public ErrorOr<Success> DeleteProductImage(Guid productImageId)
    {
        var existingImage = _images.FirstOrDefault(x => x.Id.Value == productImageId);
        
        if (existingImage is null)
        {
            return Error.NotFound($"Image with ID {productImageId} not found");
        }

        existingImage.Delete();
        
        return Result.Success;
    }

    // Add product discount
   public ErrorOr<Success> AddProductDiscount(Guid? categoryId, ProductDiscount productDiscount)
    {
        if (productDiscount is null)
        {
            return Error.Failure("Product discount cannot be null");
        }

        if (categoryId is not null)
        {
            RaiseDomainEvent(new CategoryDiscountAppliedEvent(
                categoryId.Value,
                productDiscount.Id.Value, 
                productDiscount.DiscountPercentage, 
                productDiscount.StartDate, 
                productDiscount.EndDate));
        }

        _productDiscounts.Add(productDiscount);

        return Result.Success;
    }


}
