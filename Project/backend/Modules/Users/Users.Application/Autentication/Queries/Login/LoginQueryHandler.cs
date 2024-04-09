using ErrorOr;

using MediatR;

using Users.Application.Authentication.Queries.Login;
using Users.Application.Interfaces.Autentication;
using Users.Application.Interfaces.Presistence;
using Users.Domain.UserAggregate;

namespace Users.Application.Autentication.Queries.Login;

public class LoginQueryHandler :  IRequestHandler<LoginQuery, ErrorOr<(User, string Token)>>
{
    public readonly IUserRepository _userRepository;

    public  readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginQueryHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }
    
    public async Task<ErrorOr<(User, string Token)>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        
        var userByEmail = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

        if (userByEmail is not User user)
        {
            return Error.Conflict(
                code: "user not found",
                description: "The user was not found in the system.");
        }

        if (!user.Password.Equals(request.Password))
        {
            return Error.Unauthorized(
                code: "invalid password",
                description: "The password is invalid.");
        }

        var token = _jwtTokenGenerator.GenerateToken(user);

        return (user, token);
    }
}