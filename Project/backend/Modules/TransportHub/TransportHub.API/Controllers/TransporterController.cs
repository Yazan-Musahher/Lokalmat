using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

using TransportHub.Application.Commands.RegisterTransporter;
using TransportHub.Contracts.Transporter.Register;


namespace TransportHub.API.Controllers;

[ApiController]
[Route("transporter")]
public class OrderController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public OrderController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    // Rgister transporter
    [HttpPost("register")]
    public async Task<IActionResult> CreateOrder(RegisterTransporterRequest request)
    {
        var command = _mapper.Map<RegisterTransporterCommand>(request);
        var createOrderResult = await _mediator.Send(command);
        return createOrderResult.Match<IActionResult>(
            order => Ok(_mapper.Map<RegisterTransporterResponse>(order)),
            errors => BadRequest(errors));
    }
}
