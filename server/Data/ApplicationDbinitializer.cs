using Microsoft.AspNetCore.Identity;
using server.Models.AuthModule;

namespace server.Data
{
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
            await CreateUser(userManager, "admin@uia.no", "Admin", "Adminson", "Address1", "1234567890", "Password1.", UserTypes.Admin, "Admin");
            await CreateUser(userManager, "privat@lokalmat.no", "privat", "privatLastName", "Address2", "1234567891", "Password1.", UserTypes.PrivateUser);
            await CreateUser(userManager, "produsent@lokalmat.no", "produsent", "produsentLastName", "Address3", "1234567892", "Password1.", UserTypes.Manufacturer);
            await CreateUser(userManager, "storhusholdning@lokalmat.no", "Storhus", "holdning", "Address4", "1234567893", "Password1.", UserTypes.LargeHousehold);

            // Save changes made to the database
            await db.SaveChangesAsync();
        }

        private static async Task CreateUser(UserManager<ApplicationUser> userManager, string email, string name, string lastName, string address, string phone, string password, string userType, string role = null)
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
                    UserType = userType, // Assigning string directly
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, password);

                if (result.Succeeded && role != null)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }
        }
    }
}

public static class UserTypes
{
    public const string Admin = "Admin";
    public const string PrivateUser = "PrivateUser";
    public const string Manufacturer = "Manufacturer";
    public const string LargeHousehold = "LargeHousehold";
}
