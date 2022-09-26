namespace MAUI.Core;
public static class MauiCoreExtensions
{
    public static IServiceCollection AddMAUICore(this IServiceCollection services, 
        CoreSettings settings = null)
    {
        settings ??= new CoreSettings();
        services.AddTransient<INavigationService, NavigationService>();
        services.AddTransient<ISqliteService, SqliteService>(x => new SqliteService(settings.SqliteSettings));
        services.AddHttpClient<IWebService, WebService>(x => new WebService(x, settings.WebServiceSettings))
            .AddPolicyHandler(WebService.GetRetryPolicy());
        return services;
    }
}
