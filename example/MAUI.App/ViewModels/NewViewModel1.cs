namespace MAUI.App.ViewModels;
public partial class NewViewModel1 : ViewModelBase
{
    private readonly INavigationService _navigationService;

    public NewViewModel1(IDialogService dialogService, INavigationService navigationService) : base(dialogService)
    {
        _navigationService = navigationService;
    }

    #region Commands


    [RelayCommand]
    private Task GoToView2() => _navigationService.NavigateTo<NewView2>();

    #endregion
}
