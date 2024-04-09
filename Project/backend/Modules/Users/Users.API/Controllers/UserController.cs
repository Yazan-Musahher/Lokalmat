using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Users.Application.Authentication.Commands;
using Users.Application.Authentication.Queries.Login;
using Users.Contacts.Authentication;
using Users.Contracts.Authentication;

namespace Users.API.Controllers;
[Route("users")]
public class Usercontroller : ApiController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public Usercontroller(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser(RegisterRequest request)
    {
        var command = _mapper.Map<RegisterCommand>(request);
        var result = await _mediator.Send(command);
        return result.Match<IActionResult>(
            user => Ok(_mapper.Map<AuthenticationResponse>(user)),
            errors => BadRequest(errors));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = _mapper.Map<LoginQuery>(request);
        var result = await _mediator.Send(query);
        return result.Match<IActionResult>(
            user => Ok(_mapper.Map<AuthenticationResponse>(user)),
            errors => BadRequest(errors));
    }
}