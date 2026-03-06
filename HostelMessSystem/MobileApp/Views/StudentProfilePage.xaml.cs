using MobileApp.Services;

namespace MobileApp.Views;

public partial class StudentProfilePage : ContentPage
{
    private readonly ApiService _apiService;

    public StudentProfilePage(ApiService apiService)
    {
        InitializeComponent();
        _apiService = apiService;
    }

    private async void OnLoadProfile(object sender, EventArgs e)
    {
        if (!int.TryParse(StudentIdEntry.Text, out var studentId))
        {
            ProfileEditor.Text = "Enter a valid student ID.";
            return;
        }

        var response = await _apiService.GetStudentByIdAsync(studentId);
        ProfileEditor.Text = await response.Content.ReadAsStringAsync();
    }
}
