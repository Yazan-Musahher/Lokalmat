
using Administration.Application.Interfaces.Presistence;
using Administration.Domain.AdminAggregate;
using Administration.Domain.AdminAggregate.ValueObjects;

using ErrorOr;
using MediatR;


namespace Administration.Application.Users.Commands;

public sealed class AssignRoleToUserCommandHandler : IRequestHandler<UserCommand, ErrorOr<User>>
{
    private readonly IUserRepository _userRepository;

    public AssignRoleToUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<ErrorOr<User>> Handle(UserCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var user = User.Create(
            userId: UserId.Create(request.UserId),
            request.Email,
            DateTime.UtcNow
        );

        await _userRepository.AddAsync(user, cancellationToken);
        
        return user;
    }
}