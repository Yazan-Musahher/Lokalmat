using Administration.Domain.AdminAggregate;
using ErrorOr;
using MediatR;

namespace Administration.Application.Users.Commands;


public record AsignRoleToUserCommand(
    Guid UserId,
    string Email, 
    string Role) : 
    IRequest<ErrorOr<User>>;