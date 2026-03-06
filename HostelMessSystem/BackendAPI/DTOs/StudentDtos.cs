using System.ComponentModel.DataAnnotations;

namespace BackendAPI.DTOs;

public class StudentRegisterRequest
{
    [Required, StringLength(150, MinimumLength = 2)]
    public string Name { get; set; } = string.Empty;

    [Required, StringLength(20)]
    public string RoomNumber { get; set; } = string.Empty;

    [Required, StringLength(100)]
    public string NfcCardId { get; set; } = string.Empty;

    [Required, StringLength(100)]
    public string BiometricId { get; set; } = string.Empty;
}

public record StudentResponse(int Id, string Name, string RoomNumber, string NfcCardId, string BiometricId, DateTime CreatedAt);
