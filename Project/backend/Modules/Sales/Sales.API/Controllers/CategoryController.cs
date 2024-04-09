using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

using AddCategoryCommand = Sales.Application.Categories.AddCatgegoryCommand;
using Sales.Contracts.Categories;

namespace Sales.API.Controllers.CategoryController;

[Route("categories")]
public class CategoryController : ApiController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public CategoryController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddCategory(AddCategoryRequest request)
    {
        var command = _mapper.Map<AddCategoryCommand>(request);
        var result = await _mediator.Send(command);

        return result.Match(
            category => Ok(_mapper.Map<AddCategoryResponse>(category)),
            errors => Problem(errors));
    }
}
