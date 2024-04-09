using LokalProdusert.Shared.MongoDB;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Sales.Application.Interfaces;


namespace Sales.Infrastructure.Presistence.Repositories;

public sealed class UnitOfWork : IUnitOfWork
{
    private IClientSessionHandle session { get; }
    public IDisposable Session => session;

    private List<Task> _operations { get; set; }

    public UnitOfWork(IOptions<MongoDBSettings> settings)
    {
       var mongoClient = new MongoClient(settings.Value.ConnectionURI);
        session = mongoClient.StartSession();

        _operations = new List<Task>();
    }

    public Task AddOperation(Task operation)
    {
        _operations.Add(operation);
        return Task.CompletedTask;
    }

    public void CleanOperations()
    {
        _operations.Clear();
    }


    public async Task CommitChanges(CancellationToken cancellationToken=default)
    {
        session.StartTransaction();

        foreach (var operation in _operations)
        {
            await operation;
        }

        await session.CommitTransactionAsync();

        CleanOperations();
    }

}
