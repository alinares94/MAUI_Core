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

    public async Task<bool> Delete<T>(T entity) where T : new()
    {
        using (await _mutex.LockAsync().ConfigureAwait(false))
        {
            return await _sqlConnection.DeleteAsync(entity) > 0;
        }
    }

    public async Task<IEnumerable<T>> GetAll<T>() where T : new()
    {
        using (await _mutex.LockAsync().ConfigureAwait(false))
        {
            return await _sqlConnection.Table<T>().ToListAsync();
        }
    }

    public async Task<T> GetByPk<T>(object pk) where T : new()
    {
        using (await _mutex.LockAsync().ConfigureAwait(false))
        {
            return await _sqlConnection.GetAsync<T>(pk);
        }
    }

    public async Task<T> Insert<T>(T entity) where T : new()
    {
        using (await _mutex.LockAsync().ConfigureAwait(false))
        {
            await _sqlConnection.InsertAsync(entity).ConfigureAwait(false);
            return entity;
        }
    }

    public async Task<T> Update<T>(T entity) where T : new()
    {
        using (await _mutex.LockAsync().ConfigureAwait(false))
        {
            await _sqlConnection.UpdateAsync(entity).ConfigureAwait(false);
            return entity;
        }
    }
}