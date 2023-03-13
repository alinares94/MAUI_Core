namespace MAUI.Core.Services.SecureStorage;
public interface IStorageService
{
    Task Save(string key, string value, bool secure = false);
    Task Save(KeyValuePair<string, string> value, bool secure = false);
    Task<string> Get(string key, bool secure = false);
    void Remove(string key, bool secure = false);
}
