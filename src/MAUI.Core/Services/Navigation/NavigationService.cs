namespace MAUI.Core.Services.Navigation;
public class NavigationService : INavigationService
{
    private readonly IServiceProvider _serviceProvider;
    public NavigationService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task NavigateBack()
    {
        return Application.Current.MainPage.Navigation.PopAsync();
    }

    public Task NavigateTo<TView>(IEnumerable<NavigationParam> @params = null)
        where TView : PageBase
    {
        var page = _serviceProvider.GetService<TView>();
        return Application.Current.MainPage.Navigation.PushAsync(page);
    }
}
