using Microsoft.Extensions.Logging;
using MauiApp2.Services;
using SQLite;
using System.IO;
using MauiApp2.ViewModels;

namespace MauiApp2
{
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

            // Tạo đường dẫn DB trong app data folder
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "users.db3");

            // Tạo và đăng ký SQLite connection
            var connection = new SQLiteAsyncConnection(dbPath);
            builder.Services.AddSingleton(connection);

            // Đăng ký service quản lý người dùng, truyền connection
            builder.Services.AddSingleton<UserService>();

            // Đăng ký các ViewModel
            builder.Services.AddSingleton<LoginViewModel>();
            builder.Services.AddSingleton<RegisterViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
