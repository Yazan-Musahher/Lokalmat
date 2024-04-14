using Microsoft.AspNetCore.Identity;

namespace server.Models.AuthModule;



public class ApplicationUser : IdentityUser
{
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string UserType { get; set; } // Changed from enum UserType to string
}

