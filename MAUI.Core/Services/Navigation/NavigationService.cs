namespace MAUI.Core.Services.Navigation;
public class NavigationService : INavigationService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ISqliteService _sqliteService;

    public NavigationService(IServiceProvider serviceProvider, ISqliteService sqliteService)
    {
        _serviceProvider = serviceProvider;
        _sqliteService = sqliteService;
    }

    public Task NavigateTo<TView>(IEnumerable<CustomParam> @params = null)
        where TView : Page
    {
        var page = _serviceProvider.GetService<TView>();
        return Application.Current.MainPage.Navigation.PushAsync(page);
    }

    public async Task NavigateToItem<TView, TModel>(TModel item, IEnumerable<CustomParam> @params = null)
        where TView : Page
        where TModel : ModelWithId, new()
    {
        var page = _serviceProvider.GetService<TView>();
        if (page.BindingContext is ICrudViewModelBase<TModel> vm)
        {
            await vm.LoadEntity(item);
            await vm.PostEntityLoad(@params);
        }
        await Application.Current.MainPage.Navigation.PushAsync(page);
    }

    public async Task NavigateToItem<TView, TModel>(int id, IEnumerable<CustomParam> @params = null)
        where TView : Page
        where TModel : ModelWithId, new()
    {
        var page = _serviceProvider.GetService<TView>();
        if (page.BindingContext is ICrudViewModelBase<TModel> vm)
        {
            TModel item = await _sqliteService.GetById<TModel>(id);
            await vm.LoadEntity(item);
            await vm.PostEntityLoad(@params);
        }
        await Application.Current.MainPage.Navigation.PushAsync(page);
    }
}
