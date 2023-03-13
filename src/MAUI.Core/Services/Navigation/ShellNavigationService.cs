namespace MAUI.Core.Services.Navigation;
internal class ShellNavigationService : INavigationService
{
    public Task NavigateBack()
    {
        return Shell.Current.Navigation.PopAsync();
    }

    public async Task NavigateTo<TView>(IEnumerable<NavigationParam> @params = null) where TView : PageBase
    {
        var animatedPage = Shell.Current.CurrentPage as IAnimatedPage;
        await animatedPage?.RunDisappearingAnimationAsync();

        if (@params == null)
            await Shell.Current.GoToAsync(typeof(TView).Name);
        else
            await Shell.Current.GoToAsync(typeof(TView).Name, true, @params.ToDictionary(x => x.Name, x => x.Value));

        animatedPage = Shell.Current.CurrentPage as IAnimatedPage;
        await animatedPage?.RunAppearingAnimationAsync();
        await (Shell.Current.CurrentPage.BindingContext as IViewModelBase)?.PageLoadedAsync(@params);
    }
}
