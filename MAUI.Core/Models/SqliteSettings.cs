namespace MAUI.Core.Models;
public class SqliteSettings
{
    public string DatabaseFilename { get; set; } = string.Empty;
    public IEnumerable<Type> Types { get; set; } = Enumerable.Empty<Type>();
}
