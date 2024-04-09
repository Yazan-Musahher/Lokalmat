using ErrorOr;
using MediatR;
using Users.Domain.UserAggregate;

namespace Users.Application.Authentication.Commands;


public sealed record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    DateTime CreatedAt
) : IRequest<ErrorOr<(User, string Token)>>;