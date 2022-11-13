using SQLite;
using System.Runtime.CompilerServices;

namespace MAUI.Core.Services.Sqlite;
public class AsyncLazy<T>
{
    readonly Lazy<Task<T>> instance;

    public AsyncLazy(Func<T> factory)
    {
        instance = new Lazy<Task<T>>(() => Task.Run(factory));
    }

    public AsyncLazy(Func<Task<T>> factory)
    {
        instance = new Lazy<Task<T>>(() => Task.Run(factory));
    }

    public TaskAwaiter<T> GetAwaiter()
    {
        return instance.Value.GetAwaiter();
    }
}
public class SqliteService : ISqliteService
{
    private readonly AsyncLock _mutex = new();

    public SqliteService(SqliteSettings settings)
    {
        _ = InitDatabase(settings);
    }

    private static SQLiteAsyncConnection _sqlConnection;

    public static Task InitDatabase(SqliteSettings settings)
    {
        if (string.IsNullOrEmpty(settings?.DatabaseFilename) || settings.Types == null)
            throw new NotImplementedException("You shoud implement CoreSettings");

        var directory = FileSystem.AppDataDirectory;
        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);
        var databasePath = Path.Combine(directory, settings.DatabaseFilename);
        _sqlConnection = new SQLiteAsyncConnection(databasePath);

        return _sqlConnection.CreateTablesAsync(CreateFlags.None, settings.Types.ToArray());
    }

    public async Task<bool> Delete<T>(T entity) where T : ModelWithId, new()
    {
        using (await _mutex.LockAsync().ConfigureAwait(false))
        {
            return await _sqlConnection.DeleteAsync(entity) > 0;
        }
    }

    public async Task<IEnumerable<T>> GetAll<T>() where T : ModelWithId, new()
    {
        using (await _mutex.LockAsync().ConfigureAwait(false))
        {
            return await _sqlConnection.Table<T>().ToListAsync();
        }
    }

    public async Task<T> GetById<T>(int id) where T : ModelWithId, new()
    {
        using (await _mutex.LockAsync().ConfigureAwait(false))
        {
            return await _sqlConnection.GetAsync<T>(id);
        }
    }

    public async Task<T> Save<T>(T entity) where T : ModelWithId, new()
    {
        using (await _mutex.LockAsync().ConfigureAwait(false))
        {
            if (entity.Id != 0)
            {
                await _sqlConnection.UpdateAsync(entity).ConfigureAwait(false);
                return entity;
            }
            else
            {
                await _sqlConnection.InsertAsync(entity).ConfigureAwait(false);
                return entity;
            }
        }
    }
}