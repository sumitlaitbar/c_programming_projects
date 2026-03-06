using BackendAPI.Data;
using BackendAPI.DTOs;
using BackendAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendAPI.Services;

public class AttendanceService(AppDbContext dbContext) : IAttendanceService
{
    public async Task<AttendanceResponse?> MarkNfcAsync(string nfcCardId)
    {
        var student = await dbContext.Students.FirstOrDefaultAsync(s => s.NfcCardId == nfcCardId);
        return await MarkAttendanceForStudentAsync(student, AttendanceMethod.NFC);
    }

    public async Task<AttendanceResponse?> MarkBiometricAsync(string biometricId)
    {
        var student = await dbContext.Students.FirstOrDefaultAsync(s => s.BiometricId == biometricId);
        return await MarkAttendanceForStudentAsync(student, AttendanceMethod.Biometric);
    }

    private async Task<AttendanceResponse?> MarkAttendanceForStudentAsync(Student? student, AttendanceMethod method)
    {
        if (student is null) return null;

        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var existing = await dbContext.Attendances
            .FirstOrDefaultAsync(a => a.StudentId == student.Id && a.Date == today);

        if (existing is not null)
        {
            return new AttendanceResponse(existing.Id, student.Id, student.Name, student.RoomNumber,
                existing.Date, existing.CheckInTime, existing.Method.ToString(), true);
        }

        var attendance = new Attendance
        {
            StudentId = student.Id,
            Date = today,
            CheckInTime = DateTime.UtcNow,
            Method = method
        };

        dbContext.Attendances.Add(attendance);
        await dbContext.SaveChangesAsync();

        return new AttendanceResponse(attendance.Id, student.Id, student.Name, student.RoomNumber,
            attendance.Date, attendance.CheckInTime, attendance.Method.ToString(), false);
    }

    public async Task<TodayAttendanceSummary> GetTodaySummaryAsync()
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var totalStudents = await dbContext.Students.CountAsync();

        var attendanceRecords = await dbContext.Attendances
            .Where(a => a.Date == today)
            .Include(a => a.Student)
            .OrderBy(a => a.CheckInTime)
            .Select(a => new AttendanceResponse(
                a.Id,
                a.StudentId,
                a.Student!.Name,
                a.Student.RoomNumber,
                a.Date,
                a.CheckInTime,
                a.Method.ToString(),
                false))
            .ToListAsync();

        return new TodayAttendanceSummary(
            today,
            totalStudents,
            attendanceRecords.Count,
            Math.Max(totalStudents - attendanceRecords.Count, 0),
            attendanceRecords);
    }

    public async Task<IReadOnlyCollection<AttendanceResponse>> GetHistoryAsync(int studentId)
    {
        return await dbContext.Attendances
            .Where(a => a.StudentId == studentId)
            .Include(a => a.Student)
            .OrderByDescending(a => a.Date)
            .ThenByDescending(a => a.CheckInTime)
            .Select(a => new AttendanceResponse(
                a.Id,
                a.StudentId,
                a.Student!.Name,
                a.Student.RoomNumber,
                a.Date,
                a.CheckInTime,
                a.Method.ToString(),
                false))
            .ToListAsync();
    }
}
