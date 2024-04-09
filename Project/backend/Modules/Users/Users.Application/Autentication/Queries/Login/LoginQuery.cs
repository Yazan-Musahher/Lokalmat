using ErrorOr;
using MediatR;
using Users.Domain.UserAggregate;

namespace Users.Application.Authentication.Queries.Login;

public record LoginQuery(
    string Email, 
    string Password) : IRequest<ErrorOr<(User, string Token)>>;