-- Hostel Mess Management System SQL Server schema
CREATE DATABASE HostelMessDb;
GO

USE HostelMessDb;
GO

CREATE TABLE Students (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(150) NOT NULL,
    RoomNumber NVARCHAR(20) NOT NULL,
    NFCcardId NVARCHAR(100) NOT NULL UNIQUE,
    BiometricId NVARCHAR(100) NOT NULL UNIQUE,
    CreatedAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()
);

CREATE TABLE Attendance (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    StudentId INT NOT NULL,
    [Date] DATE NOT NULL,
    CheckInTime DATETIME2 NOT NULL,
    Method NVARCHAR(20) NOT NULL CHECK (Method IN ('NFC', 'Biometric')),
    CONSTRAINT FK_Attendance_Students FOREIGN KEY (StudentId) REFERENCES Students(Id) ON DELETE CASCADE,
    CONSTRAINT UQ_Attendance_Student_Date UNIQUE (StudentId, [Date])
);

CREATE TABLE [Users] (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(500) NOT NULL,
    Role NVARCHAR(20) NOT NULL CHECK (Role IN ('Admin', 'Staff', 'Student'))
);
GO
