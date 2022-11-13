namespace MAUI.Core.Services.Web;
public interface IWebService
{
    Task<TResponse> GetAsync<TResponse>(string url, IDictionary<string, string> headers = null, string baseUrl = null)
        where TResponse : class;
    Task<TResponse> DeleteAsync<TResponse>(string url, IDictionary<string, string> headers = null, string baseUrl = null)
        where TResponse : class;
    Task<TResponse> PostAsync<TResponse, TBody>(string url, TBody body, IDictionary<string, string> headers = null, string baseUrl = null)
        where TResponse : class;
    Task<TResponse> PutAsync<TResponse, TBody>(string url, TBody body, IDictionary<string, string> headers = null, string baseUrl = null)
        where TResponse : class;
}
