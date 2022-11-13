namespace MAUI.Core.Services.Navigation;
internal class ShellNavigationService : INavigationService
{
    public Task NavigateBack()
    {
        return Shell.Current.Navigation.PopAsync();
    }

    public async Task NavigateTo<TView>(IEnumerable<CustomParam> @params = null) where TView : Page
    {
        if (Shell.Current.CurrentPage is IAnimatedPage animatedPage)
            await animatedPage.RunDisappearingAnimationAsync();
        if (@params == null)
            await Shell.Current.GoToAsync(typeof(TView).Name);
        else
            await Shell.Current.GoToAsync(typeof(TView).Name, true, @params.ToDictionary(x => x.Name, x => x.Value));
    }
}
