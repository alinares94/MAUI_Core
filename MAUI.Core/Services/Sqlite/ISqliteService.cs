namespace MAUI.Core.Services.Sqlite;
public interface ISqliteService
{
    Task<IEnumerable<T>> GetAll<T>() where T : ModelWithId, new();
    Task<T> GetById<T>(int id) where T : ModelWithId, new();
    Task<T> Save<T>(T entity) where T : ModelWithId, new();
    Task<bool> Delete<T>(T entity) where T : ModelWithId, new();
}
