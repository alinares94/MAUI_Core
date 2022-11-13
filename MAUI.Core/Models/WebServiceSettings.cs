using System.Text.Json;

namespace MAUI.Core.Models;
public class WebServiceSettings
{
    public string BaseUrl { get; set; } = string.Empty;
    public IDictionary<string, string> Headers { get; set; } 
    public JsonSerializerOptions SerializeOptions { get; set; }
}
