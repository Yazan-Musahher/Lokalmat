using ErrorOr;
using MediatR;
using Administration.Domain.AdminAggregate;

namespace Administration.Application.Users.Commands;

public record UserCommand(
    Guid UserId,
    string Email, 
    string Role) : IRequest<ErrorOr<User>>;