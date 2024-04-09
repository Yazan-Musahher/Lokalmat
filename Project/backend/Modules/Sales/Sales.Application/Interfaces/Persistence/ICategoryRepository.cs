using Sales.Domain.CategoryAggregate;

namespace Sales.Application.Interfaces.Persistence.Categories
{
    public interface ICategoryRepository
    {
        Task AddCategoryAsync(Category category, CancellationToken cancellationToken);
        Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task UpdateCategoryAsync(Category category, CancellationToken cancellationToken);
    }
}