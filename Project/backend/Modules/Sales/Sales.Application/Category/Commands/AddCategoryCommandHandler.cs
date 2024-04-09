using ErrorOr;
using MediatR;
using Sales.Application.Interfaces.Persistence.Categories;
using Sales.Domain.CategoryAggregate;

namespace Sales.Application.Categories;

public class AddCategoryCommandHandler : 
    IRequestHandler<AddCatgegoryCommand, ErrorOr<Category>>
{
    private readonly ICategoryRepository _categoryRepository;
    
    public AddCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<ErrorOr<Category>> Handle(AddCatgegoryCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        // add category name
        var category = Category.Create(command.CategoryName);

        await _categoryRepository.AddCategoryAsync(category, cancellationToken);

        return category;
    }
}
 