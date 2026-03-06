using BackendAPI.Data;
using BackendAPI.DTOs;
using BackendAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin,Staff")]
public class StudentController(AppDbContext dbContext) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult<StudentResponse>> Register([FromBody] StudentRegisterRequest request)
    {
        var duplicate = await dbContext.Students.AnyAsync(s =>
            s.NfcCardId == request.NfcCardId || s.BiometricId == request.BiometricId);

        if (duplicate)
        {
            return Conflict("NFC card or biometric ID already registered.");
        }

        var student = new Student
        {
            Name = request.Name.Trim(),
            RoomNumber = request.RoomNumber.Trim(),
            NfcCardId = request.NfcCardId.Trim(),
            BiometricId = request.BiometricId.Trim(),
            CreatedAt = DateTime.UtcNow
        };

        dbContext.Students.Add(student);
        await dbContext.SaveChangesAsync();

        return Ok(new StudentResponse(student.Id, student.Name, student.RoomNumber, student.NfcCardId, student.BiometricId, student.CreatedAt));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<StudentResponse>> GetById(int id)
    {
        var student = await dbContext.Students.FirstOrDefaultAsync(s => s.Id == id);
        if (student is null) return NotFound("Student not found.");

        return Ok(new StudentResponse(student.Id, student.Name, student.RoomNumber, student.NfcCardId, student.BiometricId, student.CreatedAt));
    }
}
