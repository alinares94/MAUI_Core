using MoreLinq;
using Polly;
using Polly.Extensions.Http;
using System.Text;
using System.Text.Json;

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
        var data = WebService.BeforeBodyRequest(body, headers);
        var response = await _httpClient.PostAsync(fullUrl, data);
        return await AfterRequest<TResponse>(headers, response);
    }

    public async Task<TResponse> PutAsync<TResponse, TBody>(string url, TBody body, IDictionary<string, string> headers = null, string baseUrl = null)
        where TResponse : class
    {
        var fullUrl = BeforeRequest(url, headers);
        var data = WebService.BeforeBodyRequest(body, headers);
        var response = await _httpClient.PutAsync(fullUrl, data);
        return await AfterRequest<TResponse>(headers, response);
    }

    private async Task<TResponse> AfterRequest<TResponse>(IDictionary<string, string> headers, HttpResponseMessage response) where TResponse : class
    {
        response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        RemoveHeaders(headers?.Select(x => x.Key));
        return DeserializeResponse<TResponse>(responseBody);
    }

    private string BeforeRequest(string url, IDictionary<string, string> headers, string baseUrl = null)
    {
        AddHeaders(headers);
        var prefix = string.IsNullOrEmpty(baseUrl) ? baseUrl : _settings.BaseUrl;
        return prefix + url;
    }

    private static StringContent BeforeBodyRequest<TBody>(TBody body, IDictionary<string, string> headers)
    {
        StringContent data = null;
        if (body != null)
        {
            var json = JsonSerializer.Serialize(body);
            data = new(json, Encoding.UTF8, "application/json");
        }

        return data;
    }

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

    private void AddHeaders(IDictionary<string, string> headers)
    {
        if (headers != null && headers.Any())
            headers.ForEach(x => _httpClient.DefaultRequestHeaders.Add(x.Key, x.Value));
    }

    private void RemoveHeaders(IEnumerable<string> headers)
    {
        if (headers != null && headers.Any())
            headers.ForEach(x => _httpClient.DefaultRequestHeaders.Remove(x));
    }
}
