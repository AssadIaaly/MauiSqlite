using MauiSqliteClassLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MauiSqlite;

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

#if DEBUG
		builder.Logging.AddDebug();
#endif
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddDbContext<ApplicationDbContext>(
            options => options.UseSqlite($"Filename={GetDatabasePath()}", x => x.MigrationsAssembly(nameof(MauiSqliteClassLibrary))));

        return builder.Build();
	}

    public static string GetDatabasePath()
    {
        var databasePath = "";
        var databaseName = "Maui.db3";

        if (DeviceInfo.Platform == DevicePlatform.Android)
        {
            databasePath = Path.Combine(FileSystem.AppDataDirectory, databaseName);
        }
        if (DeviceInfo.Platform == DevicePlatform.iOS)
        {
            SQLitePCL.Batteries_V2.Init();
            databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..", "Library", databaseName); ;
        }

        return databasePath;

    }
}
