using Mapster;

using Sales.Application.Categories;
using Sales.Contracts.Categories;
using Sales.Domain.CategoryAggregate;

namespace Sales.API.Common.Mapping.Categories;

public class CategoryMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AddCategoryRequest, AddCatgegoryCommand>()
            .Map(dest => dest.CategoryName, src => src.CategoryName);

        config.NewConfig<Category, AddCategoryResponse>()
            .Map(dest => dest.CategoryId, src => src.Id.Value)
            .Map(dest => dest.CategoryName, src => src.CategoryName);
    }
}