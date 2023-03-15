namespace MAUI.Core.ViewModels;
public interface IViewModelBase
{
    Task PageLoadedAsync(IEnumerable<NavigationParam> @params);
    void OnAppearing();
    void OnDisappearing();
}
