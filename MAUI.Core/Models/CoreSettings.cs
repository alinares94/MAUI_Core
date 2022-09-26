namespace MAUI.Core.Models;
public class CoreSettings
{
    public WebServiceSettings WebServiceSettings { get; set; } = new WebServiceSettings();
    public SqliteSettings SqliteSettings { get; set; } = new SqliteSettings();
}
