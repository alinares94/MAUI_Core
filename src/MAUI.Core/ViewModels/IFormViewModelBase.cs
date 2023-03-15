using CommunityToolkit.Mvvm.Input;

namespace MAUI.Core.ViewModels;
public interface IFormViewModelBase : IViewModelBase
{
    string Error { get; }
    bool HasErrors { get; }
    IAsyncRelayCommand SubmitCommand { get; }
}
