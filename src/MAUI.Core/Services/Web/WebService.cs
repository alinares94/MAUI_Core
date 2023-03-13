using MoreLinq;
using System.Text;

namespace MAUI.Core.Services.Web;
internal class WebService : IWebService
{
    private readonly HttpClient _httpClient;
    private readonly WebServiceSettings _settings;

    public WebService(HttpClient httpClient, WebServiceSettings settings)
    {
        _httpClient = httpClient;
        _settings = settings ?? new WebServiceSettings();
        AddHeaders(_settings.Headers);
    }

    public async Task<TResponse> GetAsync<TResponse>(string url, IDictionary<string, string> headers = null, string baseUrl = null)
        where TResponse : class
    {
        var fullUrl = BeforeRequest(url, headers);
        var response = await _httpClient.GetAsync(fullUrl);
        return await AfterRequest<TResponse>(headers, response);
    }

    public async Task<TResponse> DeleteAsync<TResponse>(string url, IDictionary<string, string> headers = null, string baseUrl = null)
    where TResponse : class
    {
        var fullUrl = BeforeRequest(url, headers);
        var response = await _httpClient.GetAsync(fullUrl);
        return await AfterRequest<TResponse>(headers, response);
    }

    public async Task<TResponse> PostAsync<TResponse, TBody>(string url, TBody body, IDictionary<string, string> headers = null, string baseUrl = null)
        where TResponse : class
    {
        var fullUrl = BeforeRequest(url, headers);
        var data = BeforeBodyRequest(body);
        var response = await _httpClient.PostAsync(fullUrl, data);
        return await AfterRequest<TResponse>(headers, response);
    }

    public async Task<TResponse> PutAsync<TResponse, TBody>(string url, TBody body, IDictionary<string, string> headers = null, string baseUrl = null)
        where TResponse : class
    {
        var fullUrl = BeforeRequest(url, headers);
        var data = BeforeBodyRequest(body);
        var response = await _httpClient.PutAsync(fullUrl, data);
        return await AfterRequest<TResponse>(headers, response);
    }

    /// <summary>
    /// Proceso posterior a la petición. Lanza una <see cref="HttpRequestException"/> si la petición es erronea
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    /// <param name="headers"></param>
    /// <param name="response"></param>
    /// <returns></returns>
    protected virtual async Task<TResponse> AfterRequest<TResponse>(IDictionary<string, string> headers, HttpResponseMessage response) where TResponse : class
    {
        response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        RemoveHeaders(headers?.Select(x => x.Key));
        return DeserializeResponse<TResponse>(responseBody);
    }

    /// <summary>
    /// Proceso anterior a la petición y a la generación del Body. Utilizado para formar la url y los header
    /// </summary>
    /// <param name="url"></param>
    /// <param name="headers"></param>
    /// <param name="baseUrl"></param>
    /// <returns></returns>
    protected virtual string BeforeRequest(string url, IDictionary<string, string> headers, string baseUrl = null)
    {
        AddHeaders(headers);
        var prefix = string.IsNullOrEmpty(baseUrl) ? baseUrl : _settings.BaseUrl;
        return prefix + url;
    }

    /// <summary>
    /// Retorna el valor del Body de la petición
    /// </summary>
    /// <typeparam name="TBody"></typeparam>
    /// <param name="body"></param>
    /// <returns></returns>
    protected virtual StringContent BeforeBodyRequest<TBody>(TBody body)
    {
        StringContent data = null;
        if (body != null)
        {
            var json = JsonSerializer.Serialize(body);
            data = new(json, Encoding.UTF8, "application/json");
        }

        return data;
    }

    /// <summary>
    /// Deserializa la respuesta
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="responseBody"></param>
    /// <returns></returns>
    private T DeserializeResponse<T>(string responseBody)
        where T : class
    {
        try
        {
            return JsonSerializer.Deserialize<T>(responseBody, _settings.SerializeOptions);
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Añade los header
    /// </summary>
    /// <param name="headers"></param>
    protected virtual void AddHeaders(IDictionary<string, string> headers)
    {
        if (headers != null && headers.Any())
            headers.ForEach(x => _httpClient.DefaultRequestHeaders.Add(x.Key, x.Value));
    }

    /// <summary>
    /// Limpia el cliente http de headers para que no existan datos corruptos en peticiones posteriores
    /// </summary>
    /// <param name="headers"></param>
    protected virtual void RemoveHeaders(IEnumerable<string> headers)
    {
        if (headers != null && headers.Any())
            headers.ForEach(x => _httpClient.DefaultRequestHeaders.Remove(x));
    }
}
