using System.ComponentModel.DataAnnotations;

namespace server.Models.AuthModule;

public class PasswordReset
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}

public class ResetPasswordModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Token { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
    public string NewPassword { get; set; }
}