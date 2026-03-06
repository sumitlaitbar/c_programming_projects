using MobileApp.Services;

namespace MobileApp.Views;

public partial class AttendanceHistoryPage : ContentPage
{
    private readonly ApiService _apiService;

    public AttendanceHistoryPage(ApiService apiService)
    {
        InitializeComponent();
        _apiService = apiService;
    }

    private async void OnLoadHistory(object sender, EventArgs e)
    {
        if (!int.TryParse(StudentIdEntry.Text, out var studentId))
        {
            HistoryEditor.Text = "Enter a valid student ID.";
            return;
        }

        var response = await _apiService.GetStudentHistoryAsync(studentId);
        HistoryEditor.Text = await response.Content.ReadAsStringAsync();
    }
}
