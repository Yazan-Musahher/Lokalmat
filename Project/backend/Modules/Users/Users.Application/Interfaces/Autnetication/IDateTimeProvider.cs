namespace Users.Application.Interfaces.Autentication;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}