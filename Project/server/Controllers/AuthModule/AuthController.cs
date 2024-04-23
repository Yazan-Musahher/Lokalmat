using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SendGrid;
using SendGrid.Helpers.Mail;
using server.Models.AuthModule;

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
                return BadRequest(new { message = "Register information is not valid" });
            }

            if (!new HashSet<string> { UserTypes.PrivateUser, UserTypes.Manufacturer, UserTypes.LargeHousehold }.Contains(model.UserType))
            {
                return BadRequest(new { message = "Invalid UserType provided." });
            }

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
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return BadRequest(ModelState);
            }

            return Ok(new { message = "User created successfully" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Login information is not valid" });
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Incorrect username or password.");
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            var token = GenerateJwtToken(user);
            return Ok(new { token = token, email = user.Email, name = user.Name, role = user.UserType,  userId = user.Id });
        }
        // start of managing request password for user
        [HttpPost("requestPasswordReset")]
        public async Task<IActionResult> RequestPasswordReset(PasswordReset model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid email address" });
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return BadRequest(new { message = "Bruker finnes ikke" });
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = $"http://localhost:3000/Password-Reset?token={Uri.EscapeDataString(token)}&email={Uri.EscapeDataString(user.Email)}";
            await SendPasswordResetEmail(model.Email, callbackUrl);

            return Ok(new { message = "Password reset link has been sent to your email address." });
        }
        
        [HttpPost("resetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid request" });
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return BadRequest(new { message = "User not found" });
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.Select(e => e.Description));
            }

            return Ok(new { message = "Password has been successfully reset." });
        }


        private async Task SendPasswordResetEmail(string email, string link)
        {
            var apiKey = _configuration["SendGrid:ApiKey"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("contact@yazan-musahher.no", "Lokalmat");
            var subject = "Reset Your Password";
            var to = new EmailAddress(email);
            var plainTextContent = $"Please reset your password by clicking here: {link}";
            var htmlContent = $"<strong>Please reset your password by clicking <a href='{link}'>here</a></strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            // Optionally handle the response
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
        public string UserType { get; set; }
    }
    public static class UserTypes
    {
        public const string PrivateUser = "PrivateUser";
        public const string Manufacturer = "Manufacturer";
        public const string LargeHousehold = "LargeHousehold";
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
