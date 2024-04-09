using Users.Domain.UserAggregate;

namespace Users.Application.Interfaces.Autentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}