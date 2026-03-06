# Hostel Mess Management System (NFC + Biometric)

Production-oriented full-stack solution containing:
- **MobileApp**: .NET MAUI Android app for admins/staff.
- **BackendAPI**: ASP.NET Core Web API with JWT + role-based auth.
- **Database**: SQL Server schema scripts.
- **Hardware**: ESP32 examples for NFC and fingerprint attendance posting.
- **ApiTests**: HTTP request examples for endpoint verification.

## 1) Project Structure

```text
HostelMessSystem/
  MobileApp/
  BackendAPI/
  Database/
  Hardware/
  ApiTests/
```

## 2) Prerequisites

- .NET SDK 8.0+
- SQL Server 2019+
- Visual Studio 2022+ with:
  - ASP.NET and web development workload
  - .NET Multi-platform App UI development workload
- Android SDK (for MAUI Android)
- Arduino IDE / PlatformIO (for ESP32)

## 3) Database Setup

1. Open SQL Server Management Studio.
2. Run:
   - `Database/create_schema.sql`
   - `Database/seed_data.sql` (replace password hashes first, or rely on EF seed in API)

## 4) Backend API Setup

1. Go to backend folder:
   ```bash
   cd HostelMessSystem/BackendAPI
   ```
2. Update `appsettings.json`:
   - SQL connection string
   - JWT key (strong secret)
3. Optional EF migrations:
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```
4. Run API:
   ```bash
   dotnet run
   ```
5. Swagger (Development):
   - `https://localhost:<port>/swagger`

### Authentication & Authorization
- `POST /api/auth/login` returns JWT.
- Roles: `Admin`, `Staff`, `Student`.
- Protected endpoints require `Authorization: Bearer <token>`.
- Student profile endpoint: `GET /api/student/{id}`.

## 5) Mobile App Setup (MAUI Android)

1. Go to app folder:
   ```bash
   cd HostelMessSystem/MobileApp
   ```
2. In `Services/ApiService.cs`, set `BaseUrl` to your API URL.
3. Build and run on Android emulator/device:
   ```bash
   dotnet build -f net8.0-android
   dotnet run -f net8.0-android
   ```

### Implemented Screens
1. Login Screen
2. Admin Dashboard
3. Student Registration Screen
4. NFC Scan Screen
5. Biometric Scan Screen
6. Attendance History Screen
7. Student Profile Screen

## 6) Attendance Business Rules

- Student registration stores: Name, RoomNumber, NFCcardId, BiometricId.
- NFC/Biometric endpoint finds student using ID.
- Attendance is marked once per day per student (`StudentId + Date` unique constraint).
- Response returns duplicate flag if already marked.
- Dashboard can fetch today's present/absent summary.

## 7) Hardware Integration

- NFC reader code: `Hardware/esp32_nfc_reader.ino`
- Fingerprint code: `Hardware/esp32_fingerprint_sensor.ino`

Update Wi-Fi credentials, API host URL, and JWT token in sketches before uploading.

## 8) API Testing Examples

Use `ApiTests/hostel_mess_api.http` in VS Code REST Client extension or convert to Postman collection.

## 9) Production Readiness Notes

- Enforce HTTPS with valid certificates.
- Store JWT key in environment variables / secret manager.
- Add refresh tokens and token revocation for advanced security.
- Add centralized logging (Serilog + sink).
- Add input validation (FluentValidation).
- Add automated tests (unit + integration).
- Add CI/CD pipeline for build, test, deploy.
- Use message queue for hardware event buffering if scale increases.

