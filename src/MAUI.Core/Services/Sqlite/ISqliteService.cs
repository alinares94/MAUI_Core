namespace MAUI.Core.Services.Sqlite;
public interface ISqliteService
{
    Task<IEnumerable<T>> GetAll<T>() where T : new();
    Task<T> GetByPk<T>(object pk) where T : new();
    Task<T> Insert<T>(T entity) where T : new();
    Task<T> Update<T>(T entity) where T : new();
    Task<bool> Delete<T>(T entity) where T : new();
}
