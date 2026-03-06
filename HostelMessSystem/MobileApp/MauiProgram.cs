using Microsoft.Extensions.Logging;
using MobileApp.Services;
using MobileApp.Views;

namespace MobileApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddSingleton<SessionService>();
        builder.Services.AddHttpClient<ApiService>();

        builder.Services.AddSingleton<AppShell>();
        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<AdminDashboardPage>();
        builder.Services.AddTransient<StudentRegistrationPage>();
        builder.Services.AddTransient<NfcScanPage>();
        builder.Services.AddTransient<BiometricScanPage>();
        builder.Services.AddTransient<AttendanceHistoryPage>();
        builder.Services.AddTransient<StudentProfilePage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
