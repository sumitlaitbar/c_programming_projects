using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace MobileApp.Services;

public class ApiService(HttpClient httpClient, SessionService sessionService)
{
    private const string BaseUrl = "https://YOUR_API_HOST";
    private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    private async Task<HttpResponseMessage> SendAsync(HttpMethod method, string path, object? payload = null, bool authorized = true)
    {
        var request = new HttpRequestMessage(method, $"{BaseUrl}{path}");
        if (authorized && sessionService.IsLoggedIn)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", sessionService.JwtToken);
        }

        if (payload is not null)
        {
            var json = JsonSerializer.Serialize(payload, JsonOptions);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        }

        return await httpClient.SendAsync(request);
    }

    public Task<HttpResponseMessage> LoginAsync(string username, string password) =>
        SendAsync(HttpMethod.Post, "/api/auth/login", new { username, password }, false);

    public Task<HttpResponseMessage> RegisterStudentAsync(object payload) =>
        SendAsync(HttpMethod.Post, "/api/student/register", payload);

    public Task<HttpResponseMessage> MarkNfcAsync(string nfcCardId) =>
        SendAsync(HttpMethod.Post, "/api/attendance/nfc", new { nfcCardId });

    public Task<HttpResponseMessage> MarkBiometricAsync(string biometricId) =>
        SendAsync(HttpMethod.Post, "/api/attendance/biometric", new { biometricId });

    public Task<HttpResponseMessage> GetTodayAttendanceAsync() =>
        SendAsync(HttpMethod.Get, "/api/attendance/today");

    public Task<HttpResponseMessage> GetStudentHistoryAsync(int studentId) =>
        SendAsync(HttpMethod.Get, $"/api/attendance/history/{studentId}");

    public Task<HttpResponseMessage> GetStudentByIdAsync(int studentId) =>
        SendAsync(HttpMethod.Get, $"/api/student/{studentId}");
}
