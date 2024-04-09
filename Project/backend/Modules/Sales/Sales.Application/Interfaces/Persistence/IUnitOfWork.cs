namespace Sales.Application.Interfaces;

public interface IUnitOfWork
{
  IDisposable Session { get; }

  Task AddOperation(Task operation);

  void CleanOperations();

  Task CommitChanges(CancellationToken cancellationToken);
}
