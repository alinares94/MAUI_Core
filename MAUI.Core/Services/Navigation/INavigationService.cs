namespace MAUI.Core.Services.Navigation;
public interface INavigationService
{
    public Task NavigateTo<TView>(IEnumerable<CustomParam> @params = null)
        where TView : Page;
    public Task NavigateToItem<TView, TModel>(TModel item, IEnumerable<CustomParam> @params = null)
        where TView : Page
        where TModel : ModelWithId, new();
    public Task NavigateToItem<TView, TModel>(int id, IEnumerable<CustomParam> @params = null)
        where TView : Page
        where TModel : ModelWithId, new();
}
