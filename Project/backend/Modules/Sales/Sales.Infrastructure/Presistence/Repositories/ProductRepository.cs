using MongoDB.Driver;
using Sales.Application.Interfaces.Persistence;
using Sales.Domain.ProductAggregate;
using ErrorOr;
using Sales.Domain.ProductAggregate.Entities;
using MongoDB.Bson;
using Mapster;
using Sales.Infrastructure.Models;
using Sales.Application.Interfaces;


namespace Sales.Infrastructure.Presistence.Repositories;

public class ProductRepository : IProductRepository
{
    
    private readonly IMongoCollection<ProductDto> _products;
    private readonly IUnitOfWork _unitOfWork;

    public ProductRepository(IMongoDatabase database, IUnitOfWork unitOfWork)
    {
        _products = database.GetCollection<ProductDto>("ProductDocument");
        _unitOfWork = unitOfWork;
    }

    public async Task AddProductAsync(Product product)
    {
        var productDto = product.Adapt<ProductDto>();
        Task operation = _products.InsertOneAsync(_unitOfWork.Session as IClientSessionHandle, productDto);
        await _unitOfWork.AddOperation(operation);
    }

    public async Task<ErrorOr<Success>> UpdateProductAsync(Product product, CancellationToken cancellationToken)
    {
        var productDto = product.Adapt<ProductDto>();
        Task operation = _products.ReplaceOneAsync(_unitOfWork.Session as IClientSessionHandle, p => p.Id == product.Id.Value, productDto, cancellationToken: cancellationToken);
        await _unitOfWork.AddOperation(operation);

        if (operation.IsCompletedSuccessfully)
        {
            return Result.Success;
        }
        else
        {
            return Error.NotFound("Product not found.");
        }
    }

    // Get Product by Id
    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var productDto = await _products.Find(p => p.Id == id).SingleOrDefaultAsync(cancellationToken);
        if (productDto == null) return null;
        return productDto.Adapt<Product>();
    }

    // Update Product details like name, price, description
    public async Task<ErrorOr<Success>> UpdateProductAsync(Guid productId,
                                string productName, 
                                decimal? unitPrice, 
                                string description, 
                                CancellationToken cancellationToken)
    {
        var filter = Builders<ProductDto>.Filter.Eq(p => p.Id, productId);
        var update = Builders<ProductDto>.Update
            .Set(p => p.ProductName, productName)
            .Set(p => p.UnitPrice, unitPrice)
            .Set(p => p.Description, description);

        var result = await _products.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
        if (result.ModifiedCount > 0)
        {
            return Result.Success;
        }
        else
        {
            return Error.NotFound("Product not found.");
        }
    }


    // Add a new image to the product
    public async Task<ErrorOr<Success>> AddProductImageAsync(Guid productId, ProductImage productImage)
    {
        var productImageDto = productImage.Adapt<ProductImageDto>();

        var update = Builders<ProductDto>.Update.Push(p => p.Images, productImageDto);
        await _products.UpdateOneAsync(p => p.Id == productId, update);

        return Result.Success;
    }


    // Update an existing image of the product
    public async Task<ErrorOr<Success>> UpdateProductImageAsync(
    Guid productId, Guid imageId, string url, string alt, string title, string contentType)
    {
        // Fetch the product by its ID
        var productDto = await _products.Find(p => p.Id == productId).SingleOrDefaultAsync();
        if (productDto == null)
        {
            return Error.NotFound($"Product with ID {productId} not found");
        }

        // Find and update the image in the list, ensuring Images is not null
        var imageToUpdate = productDto.Images?.FirstOrDefault(img => img.Id == imageId);
        if (imageToUpdate == null)
        {
            return Error.NotFound($"Image with ID {imageId} not found");
        }

        imageToUpdate.Url = url;
        imageToUpdate.Alt = alt;
        imageToUpdate.Title = title;
        imageToUpdate.ContentType = contentType;

        // Replace the entire product document
        await _products.ReplaceOneAsync(p => p.Id == productId, productDto);

        return Result.Success;
    }

  public async Task<ErrorOr<Success>> DeleteProductImageAsync(Product product, Guid imageId, CancellationToken cancellationToken)
    {
        var filter = Builders<ProductDto>.Filter.Eq(p => p.Id, product.Id.Value);

        var pullFilter = Builders<ProductDto>.Update.PullFilter("images", Builders<BsonDocument>.Filter.Eq("_id", imageId.ToString("D")));
        var result = await _products.UpdateOneAsync(filter, pullFilter, cancellationToken: cancellationToken);

        if (result.ModifiedCount > 0)
        {
            return Result.Success;
        }
        else
        {
            return Error.NotFound("Image or Product not found.");
        }
    }

    // Update Instock, OnOrder
   public async Task<ErrorOr<Success>> UpdateProductInventoryAsync(Guid productId, decimal? unitsInStock, decimal? unitsOnOrder, CancellationToken cancellationToken)
    {
        try
        {
            var productDto = await _products.Find(p => p.Id == productId).SingleOrDefaultAsync(cancellationToken);
            if (productDto == null)
            {
                return Error.NotFound("Product not found, ProductRepository Error.");
            }

            var inventories = productDto.InventoryDtos;
            if (inventories == null || !inventories.Any())
            {
                return Error.NotFound("Inventory not found, ProductRepository Error.");
            }

            var inventory = inventories.FirstOrDefault();
            if (inventory != null)
            {
                inventory.UnitsInStock = unitsInStock;
                inventory.UnitsOnOrder = unitsOnOrder;
                await _products.ReplaceOneAsync(p => p.Id == productId, productDto, cancellationToken: cancellationToken);
            }

            return Result.Success;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return Error.Failure("An error occurred while updating the product inventory.");
        }
    }

    // Get Products by CategoryId
    public async Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(Guid categoryId, CancellationToken cancellationToken)
    {
        var products = await _products.Find(p => p.CategoryId == categoryId).ToListAsync(cancellationToken);
        return products.Adapt<IEnumerable<Product>>();
    }


}
