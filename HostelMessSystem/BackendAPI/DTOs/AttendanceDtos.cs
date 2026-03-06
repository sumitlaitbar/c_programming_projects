using System.ComponentModel.DataAnnotations;

namespace BackendAPI.DTOs;

public class NfcAttendanceRequest
{
    [Required, StringLength(100)]
    public string NfcCardId { get; set; } = string.Empty;
}

public class BiometricAttendanceRequest
{
    [Required, StringLength(100)]
    public string BiometricId { get; set; } = string.Empty;
}

public record AttendanceResponse(
    int AttendanceId,
    int StudentId,
    string StudentName,
    string RoomNumber,
    DateOnly Date,
    DateTime CheckInTime,
    string Method,
    bool IsDuplicate);

public record TodayAttendanceSummary(
    DateOnly Date,
    int TotalStudents,
    int PresentCount,
    int AbsentCount,
    IReadOnlyCollection<AttendanceResponse> AttendanceRecords);
