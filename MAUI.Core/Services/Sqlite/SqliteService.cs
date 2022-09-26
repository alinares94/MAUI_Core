using MoreLinq;
using SQLite;

namespace MAUI.Core.Services.Sqlite;
public class SqliteService : ISqliteService
{
    private readonly AsyncLock _mutex = new();
    private readonly SqliteSettings _settings;


    public SqliteService(SqliteSettings settings)
    {
        _settings = settings;
        _ = InitDatabase();
    }

    protected SQLiteAsyncConnection SqlConnection { get; private set; }

    private Task InitDatabase()
    {
        if (string.IsNullOrEmpty(_settings.DatabaseFilename))
            throw new NotImplementedException("You shoud implement CoreSettings");

        var directory = FileSystem.AppDataDirectory;
        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);
        var databasePath = Path.Combine(directory, _settings.DatabaseFilename);
        SqlConnection = new SQLiteAsyncConnection(databasePath);
        return InitDatabaseTables();
    }

    public async Task InitDatabaseTables()
    {
        if (_settings?.Types == null)
            return;

        foreach (var type in _settings.Types)
            await SqlConnection.CreateTableAsync(type);
    }

    public async Task<bool> Delete<T>(T entity) where T : ModelWithId, new()
    {
        using (await _mutex.LockAsync().ConfigureAwait(false))
        {
            return await SqlConnection.DeleteAsync(entity) > 0;
        }
    }

    public async Task<IEnumerable<T>> GetAll<T>() where T : ModelWithId, new()
    {
        using (await _mutex.LockAsync().ConfigureAwait(false))
        {
            return await SqlConnection.Table<T>().ToListAsync();
        }
    }

    public async Task<T> GetById<T>(int id) where T : ModelWithId, new()
    {
        using (await _mutex.LockAsync().ConfigureAwait(false))
        {
            return await SqlConnection.GetAsync<T>(id);
        }
    }

    public async Task<T> Save<T>(T entity) where T : ModelWithId, new()
    {
        using (await _mutex.LockAsync().ConfigureAwait(false))
        {
            if (entity.Id != 0)
            {
                await SqlConnection.UpdateAsync(entity).ConfigureAwait(false);
                return entity;
            }
            else
            {
                await SqlConnection.InsertAsync(entity).ConfigureAwait(false);
                return entity;
            }
        }
    }
}