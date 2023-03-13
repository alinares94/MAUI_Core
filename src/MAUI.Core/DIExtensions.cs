using CommunityToolkit.Maui;
using Microsoft.Extensions.Configuration;
using Polly;
using Polly.Extensions.Http;
using System.Reflection;

namespace Maui.Core;

public static class DIExtensions
{
    public delegate void ActionRef<T>(ref T item);

    public static MauiAppBuilder UseMauiCore(this MauiAppBuilder builder, string settingsPath, string settingsSection, Assembly assembly = null)
    {
        builder.UseConfigurationSettings(settingsPath, assembly ?? Assembly.GetCallingAssembly());
        var settings = builder.Configuration.GetSection(settingsSection).Get<CoreSettings>();
        return RegisterServices(builder, settings ?? new CoreSettings());
    }

    public static MauiAppBuilder UseMauiCore(this MauiAppBuilder builder, ActionRef<CoreSettings> expression = null)
    {
        var settings = new CoreSettings();
        expression?.Invoke(ref settings);
        return RegisterServices(builder, settings);
    }

    private static MauiAppBuilder RegisterServices(MauiAppBuilder builder, CoreSettings settings)
    {
        builder.Services
            .AddSingleton<IDialogService, DialogService>()
            .AddNavigation(settings.NavigationSettings)
            .AddSqlite(settings.SqliteSettings)
            .AddHttp(settings.WebServiceSettings);

        return builder;
    }

    public static MauiAppBuilder UseConfigurationSettings(this MauiAppBuilder builder, string manifestPath, Assembly assembly = null)
    {
        var a = assembly ?? Assembly.GetCallingAssembly();
        using var stream = a.GetManifestResourceStream(manifestPath);
        var config = new ConfigurationBuilder().AddJsonStream(stream).Build();
        builder.Configuration.AddConfiguration(config);

        return builder;
    }

    public static IServiceCollection AddNavigation(this IServiceCollection services, NavigationSettings settings)
    {
        switch (settings.NavigationType)
        {
            case NavigationTypeEnum.Navigation:
                services.AddScoped<INavigationService, NavigationService>();
                break;
            case NavigationTypeEnum.Shell:
            default:
                services.AddScoped<INavigationService, ShellNavigationService>();
                break;
        }

        return services;
    }

    public static IServiceCollection AddSqlite(this IServiceCollection services, SqliteSettings settings)
    {
        if (string.IsNullOrEmpty(settings.DatabaseFilename))
            return services;
        var sqlite = new SqliteService(settings);
        services.AddSingleton<ISqliteService>(sqlite);

        return services;
    }

    public static IServiceCollection AddHttp(this IServiceCollection services, WebServiceSettings settings)
    {
        var policy = HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        services.AddHttpClient<IWebService, WebService>(x => new WebService(x, settings)).AddPolicyHandler(policy);

        return services;
    }
}
