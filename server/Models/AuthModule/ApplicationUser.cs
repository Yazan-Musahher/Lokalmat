using Microsoft.AspNetCore.Identity;

namespace server.Models.AuthModule;

public enum UserType
{
    PrivateUser,
    Manufacturer,
    LargeHousehold
}

public class ApplicationUser : IdentityUser
{
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public UserType UserType { get; set; } // User type property
}