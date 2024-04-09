using ErrorOr;
using Mapster;

using Sales.Application.Commands.AddDiscount;
using Sales.Application.Commands.RegisterProduct;
using Sales.Application.Commands.UpdateProduct;
using Sales.Application.Queries.GetProductDetails;
using Sales.Contracts.Product;
using Sales.Contracts.Product.Discount;
using Sales.Domain.ProductAggregate;
using Sales.Domain.ProductAggregate.Entities;

using Image = Sales.Domain.ProductAggregate.Entities.ProductImage;

namespace Sales.API.Common.Mapping;

public class RegisterProductMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateProductRequest, RegisterProductCommand>()
            .Map(dest => dest.ProductName, src => src.ProductName)
            .Map(dest => dest.UnitPrice, src => src.UnitPrice)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.CategoryId, src => src.CategoryId)
            .Map(dest => dest.ProductDiscounts, src => src.ProductDiscounts)
            .Map(dest => dest.Images, src => src.Images)
            .Map(dest => dest.Inventories, src => src.Inventories)
            .Map(dest => dest.StatusHistories, src => src.StatusHistories);
        
        config.NewConfig<Guid, GetProductDetailsQuery>()
            .MapWith(src => new GetProductDetailsQuery(src));

        config.NewConfig<Product, CreateProductResponse>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.CategoryId, src => src.CategoryId.Value);

        config.NewConfig<ProductDiscount, ProductDiscountResponse>()
            .Map(dest => dest.Id, src => src.Id.Value);
            
        config.NewConfig<Image, ImagesResponse>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.ContnetType, src => src.ContentType);
                
        
        config.NewConfig<ProductInventory, InventoryResponse>()
            .Map(dest => dest.Id, src => src.Id.Value);

        config.NewConfig<ProductStatus, ProductStatusResponse>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.Status, src => src.Status)
            .Map(dest => dest.Description, src => src.Description);
        

        // Update Product Mapping
        config.NewConfig<UpdateProductDetailsRequest, UpdateProductCommand>()
            .Map(dest => dest.ProductName, src => src.ProductName)
            .Map(dest => dest.UnitPrice, src => src.UnitPrice)
            .Map(dest => dest.Description, src => src.Description);
        
        // Add Discount Mapping
        config.NewConfig<AddProductDiscountRequest, AddProductDiscountCommand>()
            .Map(dest => dest.ProductId, src => src.ProductId)
            .Map(dest => dest.DiscountPercentage, src => src.DiscountPercentage)
            .Map(dest => dest.StartDate, src => src.StartDate)
            .Map(dest => dest.EndDate, src => src.EndDate)
            .Map(dest => dest.CategoryId, src => src.CategoryId!.Value);
        
        config.NewConfig<ProductDiscount, AddProductDiscountResponse>()
            .Map(dest => dest.DiscountId, src => src.Id.Value);

            
    }
}
