using MediatR;
using Sales.Application.Interfaces.Persistence.Categories;
using Sales.Domain.ProductAggregate.Events;
using Sales.Domain.ProductAggregate.ValueObjects;

namespace Sales.Application.Events
{
    public class ProductRegisteredEventHandler : INotificationHandler<ProductRegisteredEvent>
    {
        private readonly ICategoryRepository _categoryRepository;

        public ProductRegisteredEventHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task Handle(ProductRegisteredEvent notification, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(notification.CategoryId, cancellationToken);
            if (category is null)
            {
                throw new ApplicationException($"Category with id {notification.CategoryId} not found.");
            }
            category.AddProductId(ProductId.Create(notification.ProductId));

            await _categoryRepository.UpdateCategoryAsync(category, cancellationToken);
        }
    }
}
