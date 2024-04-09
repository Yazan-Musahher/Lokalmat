using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sales.Application.Commands.CreateOrder;
using Sales.Contracts.Order;

namespace Sales.API.Controllers;

[Route("order")]
public class OrderController : ApiController
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public OrderController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    // Create Order
    [HttpPost("create")]
    public async Task<IActionResult> CreateOrder(CreateOrderRequest request)
    {
        var command = _mapper.Map<CreateOrderCommand>(request);
        var createOrderResult = await _mediator.Send(command);
        return createOrderResult.Match(
            order => Ok(_mapper.Map<CreateOrderResponse>(order)),
            errors => Problem(errors));
    }

    // Get Order
    
}
