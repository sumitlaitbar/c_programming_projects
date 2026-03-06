namespace MobileApp.Models;

public class LoginRequest
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class StudentRegisterRequest
{
    public string Name { get; set; } = string.Empty;
    public string RoomNumber { get; set; } = string.Empty;
    public string NfcCardId { get; set; } = string.Empty;
    public string BiometricId { get; set; } = string.Empty;
}

public class AttendanceScanRequest
{
    public string NfcCardId { get; set; } = string.Empty;
    public string BiometricId { get; set; } = string.Empty;
}
