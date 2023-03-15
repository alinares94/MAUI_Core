using MAUI.Core.Settings;

namespace MAUI.App;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
        var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
			.UseMauiCore(GetSettingsPath(), CoreSettings.SettingsSectionName)
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services
			.AddScopedWithShellRoute<MainView, MainViewModel>(nameof(MainView))
			.AddScopedWithShellRoute<NewView1, NewViewModel1>(nameof(NewView1))
			.AddScopedWithShellRoute<NewView2, NewViewModel2>(nameof(NewView2));

		return builder.Build();
	}

    private static string GetSettingsPath()
    {
		return string.Format("MAUI.App.Resources.Settings.appsettings.{0}.json",
#if PRE
		"Pre"
#elif PRO
		"Pro"
#else
		"Development"
#endif
		);
	}
}
