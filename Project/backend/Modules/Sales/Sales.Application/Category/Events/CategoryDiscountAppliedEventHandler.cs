using MediatR;

using Sales.Application.Interfaces.Persistence;
using Sales.Application.Interfaces.Persistence.Categories;
using Sales.Domain.ProductAggregate.Entities;
using Sales.Domain.ProductAggregate.Events;

namespace Sales.Application.Events
{
    public class CategoryDiscountAppliedEventHandler : INotificationHandler<CategoryDiscountAppliedEvent>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;

        public CategoryDiscountAppliedEventHandler(ICategoryRepository categoryRepository, IProductRepository productRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }

        public async Task Handle(CategoryDiscountAppliedEvent notification, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(notification.CategoryId, cancellationToken);
            if (category is null)
            {
                throw new ApplicationException($"Category with id {notification.CategoryId} not found.");
            }
            var products = await _productRepository.GetProductsByCategoryIdAsync(notification.CategoryId, cancellationToken);
            if (products is null)
            {
                throw new ApplicationException($"Products with category id {notification.CategoryId} not found.");
            }
            foreach (var product in products)
            {
                bool discountAlreadyExists = product.ProductDiscounts.Any(x => x.Id.Value == notification.DiscountId);
                if (!discountAlreadyExists)
                {
                    var discount = ProductDiscount.Create(notification.DiscountPercentage, notification.StartDate, notification.EndDate);
                    product.AddProductDiscount(null, discount);
                    await _productRepository.UpdateProductAsync(product, cancellationToken);
                }

            }
        }
    }
}
