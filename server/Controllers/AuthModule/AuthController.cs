using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using server.Data;
using server.Models.AuthModule;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

namespace server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(UserSignupModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new {message = "Register information is not valid"});
            }
            
            // Check if the user already exists
            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
            {
                return BadRequest(new { message = "User already exists." });
            }

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                Name = model.Name,
                LastName = model.LastName,
                Address = model.Address,
                Phone = model.Phone,
                UserType = model.UserType,
                EmailConfirmed = true // just temporary, should implement Email confirmation for new users 
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // You might want to generate an email confirmation token here
                // and send it to the user before marking them as confirmed.
                return Ok(new { message = "User created successfully" });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            } 
            return BadRequest(ModelState);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new {message = "Login information is not valid"});
            }
            
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                var token = GenerateJwtToken(user);

                return Ok(new { token = token });
            }
            
            ModelState.AddModelError(string.Empty, "Incorrect username or password.");
            return BadRequest(ModelState);
            
        }

        private string GenerateJwtToken(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        
            // GET: /auth/currentUser
            [HttpGet("currentUser")]
            // we should add Authorize option here in production, Yazan
            public async Task<IActionResult> CurrentUser()
            {
                // The User property is populated from the JWT bearer token
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return NotFound(new { message = "User not found" });
                }

                var userModel = new
                {
                    Name = user.Name,
                    LastName = user.LastName,
                    Address = user.Address,
                    Phone = user.Phone,
                    Email = user.Email
                    
                };

                return Ok(userModel);
            }
        
        [HttpPut("updateUser")]
        public async Task<IActionResult> UpdateUser(UserUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new {message = "Update information is not valid"});
            }
            
            // Check that email and password is correct
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                return BadRequest(new {message = "Feil Passord"});
            }
            
            // Update user with new information
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (model.Name != null)
                user.Name = model.Name;
            if (model.LastName != null)
                user.LastName = model.LastName;
            if (model.Address != null)
                user.Address = model.Address;
            if (model.Phone != null)
                user.Phone = model.Phone;
            if (model.NewEmail != null)
            {
                user.Email = model.NewEmail;
                user.UserName = model.NewEmail;
                user.NormalizedEmail = model.NewEmail.ToUpper();
                user.NormalizedUserName = model.NewEmail.ToUpper();
                // This should force you to confirm your email again
            }
                
            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                return BadRequest(new {message = "User update failed"});
            }
                
            return Ok(new { message = "User updated"});
            
        }
        
        [HttpPut("updatePassword")]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new {message = "Update information is not valid"});
            }
            
            // Check that email and password is correct
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                return BadRequest(new {message = "Incorrect password"});
            }
            
            var user = await _userManager.FindByEmailAsync(model.Email);
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var updateResult = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);
            
            if (!updateResult.Succeeded)
            {
                return BadRequest(new {message = "User update failed"});
            }

            return Ok(new { token = token });
            
        }
    }

    public class UserSignupModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public UserType UserType { get; set; }
    }

    public class UserLoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    
    public class UserUpdateModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? NewEmail { get; set; }
    }

    public class UpdatePasswordModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
    }
}
