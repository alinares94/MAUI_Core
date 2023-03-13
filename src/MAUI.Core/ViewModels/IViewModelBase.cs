using System.ComponentModel;

namespace MAUI.Core.ViewModels;
public interface IViewModelBase : INotifyPropertyChanged
{
    Task PageLoadedAsync(IEnumerable<NavigationParam> @params);
    void OnAppearing();
    void OnDisappearing();
}
