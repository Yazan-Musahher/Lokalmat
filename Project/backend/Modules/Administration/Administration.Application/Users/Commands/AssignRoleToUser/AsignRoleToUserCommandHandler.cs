using Administration.Application.Interfaces.Presistence;
using Administration.Domain.AdminAggregate;
using ErrorOr;
using Administration.Application.Interfaces.Events;

using MediatR;

using Users.Domain.UserAggregate.Enums;

namespace Administration.Application.Users.Commands;


public sealed class AsignRoleToUserCommandHandler : IRequestHandler<AsignRoleToUserCommand, ErrorOr<User>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDomainEventPublisher _domainEventPublisher;


    public AsignRoleToUserCommandHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IDomainEventPublisher domainEventPublisher)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _domainEventPublisher = domainEventPublisher;
    }

    public async Task<ErrorOr<User>> Handle(AsignRoleToUserCommand command, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(command.UserId, cancellationToken);
        if (user is null)
        {
            return Error.Conflict(
                code: "user_not_found",
                description: "User not found"
            );
        }

        UserRole role = (UserRole)Enum.Parse(typeof(UserRole), command.Role);
        user.UpdateRole(role);
        await _unitOfWork.AddOperation(_userRepository.UpdateAsync(user, cancellationToken));
        await _unitOfWork.AddOperation(_domainEventPublisher.SaveEventsAsync(user, cancellationToken));
        await _unitOfWork.CommitChanges(cancellationToken);

        return user;
    }
}