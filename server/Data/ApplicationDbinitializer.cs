using Microsoft.AspNetCore.Identity;
using server.Models.AuthModule;

namespace server.Data;

public class ApplicationDbInitializer
{
    public static async Task Initialize(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        // Create roles
        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            var adminRole = new IdentityRole("Admin");
            await roleManager.CreateAsync(adminRole);
        }

        // Add standard users with specified user types
        // Note: Admin user does not pass a UserType, as it's not relevant for role-based access control
        await CreateUser(userManager, "admin@uia.no", "Admin", "Adminson", "Address1", "1234567890", "Password1.", null, "Admin");
        await CreateUser(userManager, "privat@lokalmat.no", "privat", "privatLastName", "Address2", "1234567891", "Password1.", UserType.PrivateUser);
        await CreateUser(userManager, "produsent@lokalmat.no", "produsent", "produsentLastName", "Address3", "1234567892", "Password1.", UserType.Manufacturer);
        await CreateUser(userManager, "storhusholdning@lokalmat.no", "Storhus", "holdning", "Address4", "1234567893", "Password1.", UserType.LargeHousehold);

        // Save changes made to the database
        await db.SaveChangesAsync();
    }

    private static async Task CreateUser(UserManager<ApplicationUser> userManager, string email, string name, string lastName, string address, string phone, string password, UserType? userType = null, string role = null)
    {
        if (await userManager.FindByEmailAsync(email) == null)
        {
            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                Name = name,
                LastName = lastName,
                Address = address,
                Phone = phone,
                EmailConfirmed = true
            };

            // Set the UserType only if provided (and likely not for admin users)
            if (userType.HasValue)
            {
                user.UserType = userType.Value;
            }

            var result = await userManager.CreateAsync(user, password);

            if (result.Succeeded && role != null)
            {
                await userManager.AddToRoleAsync(user, role);
            }
        }
    }
}