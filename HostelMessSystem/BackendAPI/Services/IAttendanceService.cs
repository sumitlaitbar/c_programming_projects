using BackendAPI.DTOs;

namespace BackendAPI.Services;

public interface IAttendanceService
{
    Task<AttendanceResponse?> MarkNfcAsync(string nfcCardId);
    Task<AttendanceResponse?> MarkBiometricAsync(string biometricId);
    Task<TodayAttendanceSummary> GetTodaySummaryAsync();
    Task<IReadOnlyCollection<AttendanceResponse>> GetHistoryAsync(int studentId);
}
