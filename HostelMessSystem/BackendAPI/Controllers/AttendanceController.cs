using BackendAPI.DTOs;
using BackendAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AttendanceController(IAttendanceService attendanceService) : ControllerBase
{
    [HttpPost("nfc")]
    [Authorize(Roles = "Admin,Staff")]
    public async Task<ActionResult<AttendanceResponse>> MarkNfc([FromBody] NfcAttendanceRequest request)
    {
        var result = await attendanceService.MarkNfcAsync(request.NfcCardId);
        return result is null ? NotFound("Student not found") : Ok(result);
    }

    [HttpPost("biometric")]
    [Authorize(Roles = "Admin,Staff")]
    public async Task<ActionResult<AttendanceResponse>> MarkBiometric([FromBody] BiometricAttendanceRequest request)
    {
        var result = await attendanceService.MarkBiometricAsync(request.BiometricId);
        return result is null ? NotFound("Student not found") : Ok(result);
    }

    [HttpGet("today")]
    [Authorize(Roles = "Admin,Staff")]
    public async Task<ActionResult<TodayAttendanceSummary>> GetToday()
    {
        return Ok(await attendanceService.GetTodaySummaryAsync());
    }

    [HttpGet("history/{studentId:int}")]
    [Authorize(Roles = "Admin,Staff,Student")]
    public async Task<ActionResult<IReadOnlyCollection<AttendanceResponse>>> GetHistory(int studentId)
    {
        return Ok(await attendanceService.GetHistoryAsync(studentId));
    }
}
