using Users.Application.Interfaces.Autentication;

namespace Users.Infrastructure.Autentication
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}