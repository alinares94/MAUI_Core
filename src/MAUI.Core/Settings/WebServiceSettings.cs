namespace MAUI.Core.Settings;
public class WebServiceSettings
{
    public string BaseUrl { get; set; } = string.Empty;
    public IDictionary<string, string> Headers { get; set; }
    public JsonSerializerOptions SerializeOptions { get; set; }
}
