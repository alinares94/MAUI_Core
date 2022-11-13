namespace MAUI.Core.Services.Navigation;
public interface INavigationService
{
    public Task NavigateTo<TView>(IEnumerable<CustomParam> @params = null)
        where TView : Page;
    public Task NavigateBack();
}
