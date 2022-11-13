namespace MAUI.Core.Services.Preferences;
public interface IStorageService
{
    Task Save(string key, string value, bool secure = false);
    Task Save(KeyValuePair<string, string> value, bool secure = false);
    Task<string> Get(string key, bool secure = false);
    void Remove(string key, bool secure = false);
}
