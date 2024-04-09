using Mapster;

using MongoDB.Driver;

using Users.Application.Interfaces.Presistence;
using Users.Domain.UserAggregate;
using Users.Infrastructure.Persistence.Models;

namespace Users.Infrastructure.Persistence.Repositories;

public  class UserRepository : IUserRepository
{
    private readonly IMongoCollection<UserDto> _users;
    private readonly IUnitOfWork _unitOfWork;

    public UserRepository(IMongoDatabase database, IUnitOfWork unitOfWork)
    {
        _users = database.GetCollection<UserDto>("UserDocument");
        _unitOfWork = unitOfWork;
    }

    public async Task AddAsync(User user, CancellationToken cancellationToken)
    {
        var userDto = user.Adapt<UserDto>();
        Task operation = _users.InsertOneAsync(_unitOfWork.Session as IClientSessionHandle, userDto, cancellationToken: cancellationToken);
        await _unitOfWork.AddOperation(operation);
    }


    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        var userDto = await _users.Find(u => u.Email == email).FirstOrDefaultAsync(cancellationToken);
        if (userDto == null) return null;
        return userDto.Adapt<User>();
    }

    public async Task UpdateAsync(User user, CancellationToken cancellationToken)
    {
        var userDto = user.Adapt<UserDto>();
        await _users.ReplaceOneAsync(u => u.Id == user.Id.Value, userDto, cancellationToken: cancellationToken);
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var userDto = await _users.Find(u => u.Id == id).FirstOrDefaultAsync(cancellationToken);
        if (userDto == null) return null;
        return userDto.Adapt<User>();
    }

}