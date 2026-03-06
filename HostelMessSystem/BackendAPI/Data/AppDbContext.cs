using BackendAPI.Models;
using BackendAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace BackendAPI.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Attendance> Attendances => Set<Attendance>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasIndex(s => s.NfcCardId).IsUnique();
            entity.HasIndex(s => s.BiometricId).IsUnique();
            entity.Property(s => s.Name).HasMaxLength(150).IsRequired();
            entity.Property(s => s.RoomNumber).HasMaxLength(20).IsRequired();
        });

        modelBuilder.Entity<Attendance>(entity =>
        {
            entity.HasIndex(a => new { a.StudentId, a.Date }).IsUnique();
            entity.Property(a => a.Method).HasConversion<string>().HasMaxLength(20);
            entity.HasOne(a => a.Student)
                .WithMany(s => s.Attendances)
                .HasForeignKey(a => a.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(u => u.Username).IsUnique();
            entity.Property(u => u.Role).HasConversion<string>().HasMaxLength(20);
        });

        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Username = "admin", PasswordHash = PasswordHasher.Hash("Admin@123"), Role = Role.Admin },
            new User { Id = 2, Username = "staff", PasswordHash = PasswordHasher.Hash("Staff@123"), Role = Role.Staff }
        );
    }
}
