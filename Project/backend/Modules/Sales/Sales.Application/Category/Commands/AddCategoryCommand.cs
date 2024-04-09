using ErrorOr;
using MediatR;
using Sales.Domain.CategoryAggregate;

namespace Sales.Application.Categories;

public record AddCatgegoryCommand(
    string CategoryName
) : IRequest<ErrorOr<Category>>;