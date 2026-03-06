using MobileApp.Services;

namespace MobileApp.Views;

public partial class StudentRegistrationPage : ContentPage
{
    private readonly ApiService _apiService;

    public StudentRegistrationPage(ApiService apiService)
    {
        InitializeComponent();
        _apiService = apiService;
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        StatusLabel.Text = string.Empty;
        var response = await _apiService.RegisterStudentAsync(new
        {
            name = NameEntry.Text,
            roomNumber = RoomEntry.Text,
            nfcCardId = NfcEntry.Text,
            biometricId = BioEntry.Text
        });

        StatusLabel.Text = response.IsSuccessStatusCode ? "Student registered successfully." : await response.Content.ReadAsStringAsync();
    }
}
