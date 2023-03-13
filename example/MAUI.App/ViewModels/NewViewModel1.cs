using CommunityToolkit.Mvvm.Input;
using MAUI.App.Views;
using MAUI.Core.Services.Navigation;

namespace MAUI.App.ViewModels;
public class NewViewModel1 : ViewModelBase
{
    private readonly INavigationService _navigationService;

    public NewViewModel1(IDialogService dialogService, INavigationService navigationService) : base(dialogService)
    {
        _navigationService = navigationService;
    }

    #region Commands

    public IAsyncRelayCommand GoToView2Command { get; set; }

    protected override void RegisterCommands()
    {
        base.RegisterCommands();

        GoToView2Command = new AsyncRelayCommand(GoToView2);
    }

    #endregion

    #region Methods

    private async Task GoToView2()
    {
        await _navigationService.NavigateTo<NewView2>();
    }

    #endregion
}
