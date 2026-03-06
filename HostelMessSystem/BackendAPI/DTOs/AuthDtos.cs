using System.ComponentModel.DataAnnotations;

namespace BackendAPI.DTOs;

public class LoginRequest
{
    [Required]
    public string Username { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}

public record LoginResponse(string Token, string Role, string Username);
