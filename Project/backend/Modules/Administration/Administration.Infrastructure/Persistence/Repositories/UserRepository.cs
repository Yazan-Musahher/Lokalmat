using Administration.Application.Interfaces.Presistence;
using Administration.Domain.AdminAggregate;
using Administration.Infrastructure.Persistence.Models;

using Mapster;

using MongoDB.Driver;

namespace Administration.Infrastructure.Persistence.Repositories;

public  class UserRepository : IUserRepository
{
    private readonly IMongoCollection<UserDto> _users;
    private readonly IUnitOfWork _unitOfWork;

    public UserRepository(IMongoDatabase database, IUnitOfWork unitOfWork)
    {
        _users = database.GetCollection<UserDto>("AdminUserDocument");
        _unitOfWork = unitOfWork;
    }

    public async Task AddAsync(User user, CancellationToken cancellationToken)
    {
        var userDto = user.Adapt<UserDto>();
        await _users.InsertOneAsync(userDto, cancellationToken: cancellationToken);
    }


    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        var userDto = await _users.Find(u => u.Email == email).FirstOrDefaultAsync(cancellationToken);
        if (userDto == null) return null;
        return userDto.Adapt<User>();
    }

    public Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _users.Find(u => u.Id == id).FirstOrDefaultAsync(cancellationToken).ContinueWith(t => t.Result.Adapt<User>(), cancellationToken);
    }

    public async Task UpdateAsync(User user, CancellationToken cancellationToken)
    {
        var userDto = user.Adapt<UserDto>();
        Task opertion = _users.ReplaceOneAsync(_unitOfWork.Session as IClientSessionHandle, u => u.Id == user.Id.Value, userDto, cancellationToken: cancellationToken);
        await _unitOfWork.AddOperation(opertion);
        
        //await _users.ReplaceOneAsync(u => u.Id == user.Id.Value, userDto, cancellationToken: cancellationToken);
    }

    

}