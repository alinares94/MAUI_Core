namespace MAUI.Core.ViewModels;
public abstract class CrudViewModelBase<T> : ViewModelBase, ICrudViewModelBase<T>
    where T : ModelWithId
{
    public T Entity { get; set; }

    public virtual Task LoadEntity(T entity)
    {
        Entity = entity;
        return Task.CompletedTask;
    }

    public virtual Task PostEntityLoad(IEnumerable<CustomParam> @params)
    {
        return Task.CompletedTask;
    }
}
