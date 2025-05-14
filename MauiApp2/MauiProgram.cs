using MauiApp2.Services;
using Microsoft.Extensions.Logging;

namespace MauiApp2;

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
            });

        // Đăng ký DatabaseHelper
        builder.Services.AddSingleton<DatabaseHelper>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}