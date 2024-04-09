using ErrorOr;
using MediatR;
using Users.Application.Interfaces.Autentication;
using Users.Application.Interfaces.Presistence;
using Users.Domain.UserAggregate;
using Users.Application.Interfaces.Events;
using Users.Domain.UserAggregate.Enums;

namespace Users.Application.Authentication.Commands;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<(User, string Token)>>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IDomainEventPublisher _domainEventPublisher;
    private readonly IUnitOfWork _unitOfWork;


    public RegisterCommandHandler(
        IUserRepository userRepository,
        IJwtTokenGenerator jwtTokenGenerator,
        IDomainEventPublisher domainEventPublisher,
        IUnitOfWork unitOfWork
    )
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
        _domainEventPublisher = domainEventPublisher;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<(User, string Token)>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var userByEmail = await _userRepository.GetByEmailAsync(command.Email, cancellationToken);

        if (userByEmail is not null)
        {
            return Error.Conflict(
                code: "email already registered",
                description: "The email is already registered in the system."
            );
        }
       
        var user = User.Create(
            command.FirstName,
            command.LastName,
            command.Email,
            command.Password,
            UserRole.Customer,
            DateTime.UtcNow
        );

        await _unitOfWork.AddOperation(_userRepository.AddAsync(user, cancellationToken));
        await _unitOfWork.AddOperation(_domainEventPublisher.SaveEventsAsync(user, cancellationToken));
        await _unitOfWork.CommitChanges(cancellationToken);

        var token = _jwtTokenGenerator.GenerateToken(user);

        return (user, token);
    }
}