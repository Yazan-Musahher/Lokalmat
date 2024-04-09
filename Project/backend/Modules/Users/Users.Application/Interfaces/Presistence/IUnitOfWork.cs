namespace Users.Application.Interfaces.Presistence;

public interface IUnitOfWork
{
  IDisposable Session { get; }

  Task AddOperation(Task operation);

  void CleanOperations();

  Task CommitChanges(CancellationToken cancellationToken);
}
