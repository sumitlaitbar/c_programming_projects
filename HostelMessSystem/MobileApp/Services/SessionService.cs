namespace MobileApp.Services;

public class SessionService
{
    public string JwtToken { get; private set; } = string.Empty;
    public string Username { get; private set; } = string.Empty;
    public string Role { get; private set; } = string.Empty;

    public bool IsLoggedIn => !string.IsNullOrWhiteSpace(JwtToken);

    public void SetSession(string token, string username, string role)
    {
        JwtToken = token;
        Username = username;
        Role = role;
    }

    public void Clear()
    {
        JwtToken = string.Empty;
        Username = string.Empty;
        Role = string.Empty;
    }
}
