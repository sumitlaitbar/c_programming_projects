using MobileApp.Services;

namespace MobileApp.Views;

public partial class BiometricScanPage : ContentPage
{
    private readonly ApiService _apiService;

    public BiometricScanPage(ApiService apiService)
    {
        InitializeComponent();
        _apiService = apiService;
    }

    private async void OnMarkBiometric(object sender, EventArgs e)
    {
        var response = await _apiService.MarkBiometricAsync(BiometricIdEntry.Text ?? string.Empty);
        ResultLabel.Text = response.IsSuccessStatusCode ? "Attendance marked via biometric." : await response.Content.ReadAsStringAsync();
    }
}
