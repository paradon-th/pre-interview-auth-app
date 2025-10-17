using System.ComponentModel.DataAnnotations;

namespace PreAuthBe.DTOs;

public class RegisterDto
{
    [Required]
    public required string Username { get; set; }

    [Required]
    public required string Password { get; set; }
    
    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}