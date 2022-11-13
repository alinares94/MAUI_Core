namespace MAUI.Core.ViewModels;
public interface ICrudViewModelBase<T> : IViewModelBase
    where T : ModelWithId
{
    public T Entity { get; set; }
    Task LoadEntity(T entity);
    Task PostEntityLoad(IEnumerable<CustomParam> @params);
}
