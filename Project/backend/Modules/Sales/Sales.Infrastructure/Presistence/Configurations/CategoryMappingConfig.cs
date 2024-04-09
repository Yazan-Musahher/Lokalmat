using Mapster;

using Sales.Domain.CategoryAggregate;
using Sales.Domain.CategoryAggregate.ValueObjects;
using Sales.Domain.ProductAggregate.ValueObjects;
using Sales.Infrastructure.Models.Categories;

namespace Sales.Infrastructure.Presistence.Configurations;

public class CategoryMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Category, CategoryDto>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.CategoryName, src => src.CategoryName)
            .Map(dest => dest.ProductIds, src => src.ProductIds.Select(x => x.Value).ToList());

        config.NewConfig<CategoryDto, Category>()
            .MapWith(dto => Category.Create(
                CategoryId.Create(dto.Id),
                dto.CategoryName ?? string.Empty,
                dto.ProductIds.Select(x => ProductId.Create(x)).ToList()));

    }
}