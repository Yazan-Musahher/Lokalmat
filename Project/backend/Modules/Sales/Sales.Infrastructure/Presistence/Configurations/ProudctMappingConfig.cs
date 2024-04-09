using Mapster;
using Sales.Domain.ProductAggregate;
using Sales.Domain.ProductAggregate.Entities;
using Sales.Domain.ProductAggregate.ValueObjects;
using Sales.Infrastructure.Models;
using Sales.Domain.CategoryAggregate.ValueObjects;

namespace Sales.Infrastructure.Persistence.Configurations;

public class ProductMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ProductDiscount, ProductDiscountDto>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.DiscountPercentage, src => src.DiscountPercentage)
            .Map(dest => dest.StartDate, src => src.StartDate)
            .Map(dest => dest.EndDate, src => src.EndDate);
        
        config.NewConfig<ProductDiscountDto, ProductDiscount>()
            .MapWith(dto => ProductDiscount.Create(
                ProductDiscountId.Create(dto.Id),
                dto.DiscountPercentage,
                dto.StartDate,
                dto.EndDate
                ));

        config.NewConfig<ProductImage, ProductImageDto>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.Url, src => src.Url)
            .Map(dest => dest.Alt, src => src.Alt)
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.ContentType, src => src.ContentType);

        config.NewConfig<ProductImageDto, ProductImage>()
            .MapWith(dto => ProductImage.Create(
                ProductImageId.Create(dto.Id),
                dto.Url ?? string.Empty,
                dto.Alt ?? string.Empty,
                dto.Title ?? string.Empty,
                dto.ContentType ?? string.Empty));
        
        config.NewConfig<ProductInventory, ProductInventoryDto>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.UnitsInStock, src => src.UnitsInStock)
            .Map(dest => dest.UnitsOnOrder, src => src.UnitsOnOrder)
            .Map(dest => dest.ReorderLevel, src => src.ReorderLevel)
            .Map(dest => dest.Location, src => src.Location);
        
        config.NewConfig<ProductInventoryDto, ProductInventory>()
            .MapWith(dto => ProductInventory.Create(
                ProductInventoryId.Create(dto.Id),
                dto.UnitsInStock?? 0,
                dto.UnitsOnOrder?? 0,
                dto.ReorderLevel?? 0,
                dto.Location ?? string.Empty));
        
        config.NewConfig<ProductStatus, ProductStatusDto>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.Status, src => src.Status)
            .Map(dest => dest.Description, src => src.Description);
        
        config.NewConfig<ProductStatusDto, ProductStatus>()
            .MapWith(dto => ProductStatus.Create(
                ProductStatusId.Create(dto.Id),
                dto.Status,
                dto.Description ?? string.Empty));

        config.NewConfig<Product, ProductDto>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.ProductName, src => src.ProductName)
            .Map(dest => dest.UnitPrice, src => src.UnitPrice)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.CategoryId, src => src.CategoryId.Value)
            .Map(dest => dest.ProductDiscountDtos, src => src.ProductDiscounts != null 
                ? src.ProductDiscounts.Select(category => category.Adapt<ProductDiscountDto>()).ToList() 
                : null)
            .Map(dest => dest.Images, src => src.Images != null 
                ? src.Images.Select(image => image.Adapt<ProductImageDto>()).ToList() 
                : null)
            .Map(dest => dest.InventoryDtos, src => src.Inventories != null
                ? src.Inventories.Select(inventory => inventory.Adapt<ProductInventoryDto>()).ToList()
                : null)
            .Map(dest => dest.StatusDtos, src => src.StatusHistories != null
                ? src.StatusHistories.Select(status => status.Adapt<ProductStatusDto>()).ToList()
                : null)
            .Map(dest => dest.CreatedDateTime, src => src.CreatedDateTime);

        config.NewConfig<ProductDto, Product>()
            .MapWith(dto => Product.Create(
                ProductId.Create(dto.Id),
                dto.ProductName ?? string.Empty,
                dto.UnitPrice ?? 0,
                dto.Description ?? string.Empty,
                CategoryId.Create(dto.CategoryId),
                dto.Images != null ? dto.Images.Select(
                    imgDto => imgDto.Adapt<ProductImage>()).ToList() : new List<ProductImage>(),
                dto.InventoryDtos != null ? dto.InventoryDtos.Select(
                    invDto => invDto.Adapt<ProductInventory>()).ToList() : new List<ProductInventory>(),
                dto.StatusDtos != null ? dto.StatusDtos.Select(
                    statusDto => statusDto.Adapt<ProductStatus>()).ToList() : new List<ProductStatus>(),
                dto.ProductDiscountDtos != null ? dto.ProductDiscountDtos.Select(
                    catDto => catDto.Adapt<ProductDiscount>()).ToList() : new List<ProductDiscount>()
                ));

    }
}
