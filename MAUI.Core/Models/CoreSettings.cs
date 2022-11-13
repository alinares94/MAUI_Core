namespace MAUI.Core.Models;
public class CoreSettings
{
    public WebServiceSettings WebServiceSettings { get; set; } = new();
    public SqliteSettings SqliteSettings { get; set; } = new();
    public NavigationSettings NavigationSettings { get; set; } = new();
}
