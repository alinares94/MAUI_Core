namespace MAUI.Core.Services.Preferences;
public class StorageService : IStorageService
{
    private readonly ISecureStorage _secureStorage;
    private readonly IPreferences _preferences;

    public StorageService(IPreferences preferences, ISecureStorage secureStorage)
    {
        _preferences = preferences;
        _secureStorage = secureStorage;
    }

    public async Task Save(string key, string value, bool secure = false)
    {
        if (secure)
            await _secureStorage.SetAsync(key, value);
        else
            _preferences.Set(key, value);
    }
    public async Task Save(KeyValuePair<string, string> value, bool secure = false)
    {
        await Save(value.Key, value.Value, secure);
    }

    public async Task<string> Get(string key, bool secure = false)
    {
        if (secure)
            return await _secureStorage.GetAsync(key);
        else
            return _preferences.Get(key, string.Empty);
    }

    public void Remove(string key, bool secure = false)
    {
        if (secure)
            _secureStorage.Remove(key);
        else
            _preferences.Remove(key);
    }
}
