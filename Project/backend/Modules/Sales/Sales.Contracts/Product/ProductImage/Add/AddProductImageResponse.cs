namespace Sales.Contracts.Product.ProductImage.Add;

public record AddProductImageResponse( 
    string ProductId,
    string Id,
    string Url, 
    string Alt,
    string Title,
    string ContentType
    );
