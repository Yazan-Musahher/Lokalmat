using Administration.Application.Users.Commands;
using Administration.Contracts.UserRoleAssignment;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Administration.API.Controllers;

[Route("admin")]
public class AdminController : ApiController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public AdminController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("asignRoleToUser")]
    public async Task<IActionResult> AsignRoleToUser(AssignRoleToUserRequest request)
    {
        var command = _mapper.Map<AsignRoleToUserCommand>(request);
        var result = await _mediator.Send(command);

        return result.Match(
            user => Ok(_mapper.Map<AssignRoleToUserResponse>(user)),
            errors => Problem(errors));
    }

}