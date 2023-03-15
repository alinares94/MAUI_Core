namespace MAUI.Core.Settings;
public class CoreSettings
{
    public const string SettingsSectionName = nameof(CoreSettings);
    public NavigationSettings NavigationSettings { get; set; } = new NavigationSettings();
    public SqliteSettings SqliteSettings { get; set; } = new SqliteSettings();
    public WebServiceSettings WebServiceSettings { get; set; } = new WebServiceSettings();
}
