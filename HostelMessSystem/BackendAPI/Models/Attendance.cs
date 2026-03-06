namespace BackendAPI.Models;

public class Attendance
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public DateOnly Date { get; set; }
    public DateTime CheckInTime { get; set; }
    public AttendanceMethod Method { get; set; }

    public Student? Student { get; set; }
}
