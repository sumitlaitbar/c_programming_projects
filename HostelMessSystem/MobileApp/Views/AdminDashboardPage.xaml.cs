using System.Text.Json;
using MobileApp.Services;

namespace MobileApp.Views;

public partial class AdminDashboardPage : ContentPage
{
    private readonly ApiService _apiService;

    public AdminDashboardPage(ApiService apiService)
    {
        InitializeComponent();
        _apiService = apiService;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadSummary();
    }

    private async Task LoadSummary()
    {
        var response = await _apiService.GetTodayAttendanceAsync();
        if (!response.IsSuccessStatusCode)
        {
            SummaryLabel.Text = "Unable to load summary.";
            return;
        }

        var payload = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(payload);
        var root = doc.RootElement;
        SummaryLabel.Text = $"Date: {root.GetProperty("date").GetString()}\n" +
                            $"Present: {root.GetProperty("presentCount").GetInt32()}\n" +
                            $"Absent: {root.GetProperty("absentCount").GetInt32()}\n" +
                            $"Total: {root.GetProperty("totalStudents").GetInt32()}";
    }

    private async void OnRefresh(object sender, EventArgs e) => await LoadSummary();
    private async void OnGoRegister(object sender, EventArgs e) => await Shell.Current.GoToAsync("//StudentRegistrationPage");
    private async void OnGoNfc(object sender, EventArgs e) => await Shell.Current.GoToAsync("//NfcScanPage");
    private async void OnGoBio(object sender, EventArgs e) => await Shell.Current.GoToAsync("//BiometricScanPage");
}
