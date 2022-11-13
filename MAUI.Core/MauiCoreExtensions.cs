
using Polly;
using Polly.Extensions.Http;

namespace MAUI.Core;
public static class MauiCoreExtensions
{
    public static IServiceCollection AddMAUICore(this IServiceCollection services, Action<CoreSettings> configure = null)
    {
        CoreSettings settings = new();
        configure?.Invoke(settings);


        services.AddNavigation(settings);
        services.AddHttp(settings);
        services.AddSqlite(settings);

        services.AddSingleton<IDialogService, DialogService>();
        services.AddSingleton<IStorageService, StorageService>();
        services.AddSingleton(SecureStorage.Default);
        services.AddSingleton(Preferences.Default);
        return services;
    }

    private static IServiceCollection AddSqlite(this IServiceCollection services, CoreSettings settings)
    {
        if (string.IsNullOrEmpty(settings.SqliteSettings.DatabaseFilename))
            return services;
        var sqlite = new SqliteService(settings.SqliteSettings);
        services.AddSingleton<ISqliteService>(sqlite);

        return services;
    }
    private static IServiceCollection AddHttp(this IServiceCollection services, CoreSettings settings)
    {
        var policy = HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        services.AddHttpClient<IWebService, WebService>(x => new WebService(x, settings.WebServiceSettings))
            .AddPolicyHandler(policy);

        return services;
    }
    private static IServiceCollection AddNavigation(this IServiceCollection services, CoreSettings settings)
    {
        return settings.NavigationSettings.NavigationTypeEnum == NavigationTypeEnum.Shell
            ? services.AddTransient<INavigationService, ShellNavigationService>()
            : services.AddTransient<INavigationService, NavigationService>();
    }
}
