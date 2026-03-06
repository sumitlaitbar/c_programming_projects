USE HostelMessDb;
GO

-- Password hashes should be generated from API PasswordHasher logic.
INSERT INTO [Users](Username, PasswordHash, Role)
VALUES
('admin', 'REPLACE_WITH_HASH_FOR_Admin@123', 'Admin'),
('staff', 'REPLACE_WITH_HASH_FOR_Staff@123', 'Staff');
GO
