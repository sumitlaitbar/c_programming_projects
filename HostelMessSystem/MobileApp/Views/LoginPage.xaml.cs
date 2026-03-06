using System.Text.Json;
using MobileApp.Services;

namespace MobileApp.Views;

public partial class LoginPage : ContentPage
{
    private readonly ApiService _apiService;
    private readonly SessionService _sessionService;

    public LoginPage(ApiService apiService, SessionService sessionService)
    {
        InitializeComponent();
        _apiService = apiService;
        _sessionService = sessionService;
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        StatusLabel.Text = string.Empty;
        var response = await _apiService.LoginAsync(UsernameEntry.Text ?? string.Empty, PasswordEntry.Text ?? string.Empty);
        if (!response.IsSuccessStatusCode)
        {
            StatusLabel.Text = "Login failed. Check username/password.";
            return;
        }

        var content = await response.Content.ReadAsStringAsync();
        using var json = JsonDocument.Parse(content);
        _sessionService.SetSession(
            json.RootElement.GetProperty("token").GetString() ?? string.Empty,
            json.RootElement.GetProperty("username").GetString() ?? string.Empty,
            json.RootElement.GetProperty("role").GetString() ?? string.Empty);

        await Shell.Current.GoToAsync("//AdminDashboardPage");
    }
}
