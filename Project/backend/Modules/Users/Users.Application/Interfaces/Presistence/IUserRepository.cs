using Users.Domain.UserAggregate;

namespace Users.Application.Interfaces.Presistence
{
    public interface IUserRepository
    {
        Task AddAsync(User user, CancellationToken cancellationToken);
        Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);

        Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task UpdateAsync(User user, CancellationToken cancellationToken);
    }
}