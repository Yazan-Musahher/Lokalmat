using Sales.Domain.CategoryAggregate.ValueObjects;
using Sales.Domain.Models;
using Sales.Domain.ProductAggregate.ValueObjects;

namespace Sales.Domain.CategoryAggregate;

public sealed class Category : AggregateRoot<CategoryId>
{
    public string? CategoryName { get; private set; }

    public List<ProductId> ProductIds { get; private set; } = new();

    public IReadOnlyCollection<ProductId> GetProducts() => ProductIds.AsReadOnly();

    private Category(CategoryId categoryId, string categoryName, 
        List<ProductId>? productIds = null) : base(categoryId)
    {
        if (string.IsNullOrWhiteSpace(categoryName))
        {
            throw new ArgumentException("Category name cannot be empty", nameof(categoryName));
        }

        CategoryName = categoryName;
        ProductIds = productIds ?? new List<ProductId>();
    }

    public static Category Create(string categoryName)
    {
        return new Category(
            CategoryId.CreateUnique(),
            categoryName,
            new List<ProductId>()
            
        );
    }

    public static Category Create(
        CategoryId categoryId, 
        string categoryName,
        List<ProductId>? productIds = null)
    {
        return new Category(
            categoryId,
            categoryName,
            productIds ?? new List<ProductId>()
        );
    }

    // Add product to category constructor
    

    public void AddProductId(ProductId productId)
    {
        if (ProductIds.Contains(productId))
        {
            throw new InvalidOperationException("Product already exists in the category");
        }

        ProductIds.Add(productId);
    }
}