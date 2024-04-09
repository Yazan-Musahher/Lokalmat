using Mapster;
using MongoDB.Driver;
using Sales.Application.Interfaces;
using Sales.Application.Interfaces.Persistence.Categories;
using Sales.Domain.CategoryAggregate;
using Sales.Infrastructure.Models.Categories;

namespace Sales.Infrastructure.Presistence.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly IMongoCollection<CategoryDto> _categories;
    private readonly IUnitOfWork _unitOfWork;
    public CategoryRepository(IMongoDatabase database, IUnitOfWork unitOfWork)
    {
        _categories = database.GetCollection<CategoryDto>("CategoryDocument");
        _unitOfWork = unitOfWork;
    }

    public async Task AddCategoryAsync(Category category, CancellationToken cancellationToken)
    {
       //add category to the database
        var categoryDto = category.Adapt<CategoryDto>();
        Task operation = _categories.InsertOneAsync(_unitOfWork.Session as IClientSessionHandle, categoryDto);
        await _unitOfWork.AddOperation(operation);
    }

    public async Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var categoryDto = await _categories.Find(c => c.Id == id).FirstOrDefaultAsync(cancellationToken);
        return categoryDto?.Adapt<Category>();
    }

    public async Task UpdateCategoryAsync(Category category, CancellationToken cancellationToken)
    {
        var categoryDto = category.Adapt<CategoryDto>();
        Task operation = _categories.ReplaceOneAsync(c => c.Id == category.Id.Value, categoryDto, cancellationToken: cancellationToken);
        await _unitOfWork.AddOperation(operation);
    }

}