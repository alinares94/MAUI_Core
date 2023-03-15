namespace MAUI.Core.Services.Navigation;
public interface INavigationService
{
    public Task NavigateTo<TView>(IEnumerable<NavigationParam> @params = null)
        where TView : PageBase;
    public Task NavigateBack();
}
