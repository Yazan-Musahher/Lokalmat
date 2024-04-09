using Sales.Domain.Models;
using Sales.Domain.ProductAggregate.ValueObjects;

namespace Sales.Domain.ProductAggregate.Entities;

public sealed class ProductImage : Entity<ProductImageId>
{
    public string? Url { get; private set; }
    public string? Alt { get; private set; }
    public string? Title { get; private set; }
    public string? ContentType { get; private set; }

    private ProductImage(ProductImageId imageId, string url, string alt, string title, string contentType) :
        base(imageId)
    {
        if (string.IsNullOrWhiteSpace(url))
        {
            throw new ArgumentException("Image url cannot be empty", nameof(url));
        }

        if (string.IsNullOrWhiteSpace(alt))
        {
            throw new ArgumentException("Image alt cannot be empty", nameof(alt));
        }

        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Image title cannot be empty", nameof(title));
        }

        if (string.IsNullOrWhiteSpace(contentType))
        {
            throw new ArgumentException("Image content type cannot be empty", nameof(contentType));
        }
        Id = imageId;
        Url = url;
        Alt = alt;
        Title = title;
        ContentType = contentType;
    }

    public static ProductImage Create(
        ProductImageId imageId,
        string url,
        string alt,
        string title,
        string contentType)
    {
        return new(
            imageId,
            url,
            alt,
            title,
            contentType);
    }

    public static ProductImage Create(
        string url,
        string alt,
        string title,
        string contentType)
    {
        return new(
            ProductImageId.CreateUnique(), 
            url,
            alt,
            title,
            contentType);
    }

    public void UpdateDetails(
        string url,
        string alt,
        string title,
        string contentType)
    {
        Url = url;
        Alt = alt;
        Title = title;
        ContentType = contentType;
    }

    public void Delete()
    {
        Url = null;
        Alt = null;
        Title = null;
        ContentType = null;
    }
  
    

}
