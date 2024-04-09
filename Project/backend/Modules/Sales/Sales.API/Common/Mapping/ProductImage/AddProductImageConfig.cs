using Mapster;
using Sales.Application.Commands.AddProductImage;
using Sales.Application.Commands.AddProductImage.Add;
using Sales.Contracts.Product.ProductImage.Add;
using Sales.Contracts.Product.ProductImage.Update;
using Sales.Domain.ProductAggregate.Entities;
namespace Sales.API.Common.Mapping;

public class AddProductImageConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<AddProductImageRequest, AddProductImageCommand>()
            .Map(dest => dest.Url, src => src.Url)
            .Map(dest => dest.Alt, src => src.Alt)
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.ContentType, src => src.ContentType);
    
        // Map the response
        config.ForType<ProductImage, AddProductImageResponse>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.Url, src => src.Url)
            .Map(dest => dest.Alt, src => src.Alt)
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.ContentType, src => src.ContentType);

        // Map the update product image request
        config.ForType<UpdateProductImageRequest, UpdateProductImageCommand>()
            .Map(dest => dest.Url, src => src.Url)
            .Map(dest => dest.Alt, src => src.Alt)
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.ContentType, src => src.ContentType);
    }
}
