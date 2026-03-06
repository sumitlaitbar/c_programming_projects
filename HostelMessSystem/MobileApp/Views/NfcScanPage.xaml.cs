using MobileApp.Services;

namespace MobileApp.Views;

public partial class NfcScanPage : ContentPage
{
    private readonly ApiService _apiService;

    public NfcScanPage(ApiService apiService)
    {
        InitializeComponent();
        _apiService = apiService;
    }

    private async void OnMarkNfc(object sender, EventArgs e)
    {
        var response = await _apiService.MarkNfcAsync(NfcIdEntry.Text ?? string.Empty);
        ResultLabel.Text = response.IsSuccessStatusCode ? "Attendance marked via NFC." : await response.Content.ReadAsStringAsync();
    }
}
