namespace MAUI.App.ViewModels;
public partial class MainViewModel : FormViewModelBase
{
    public MainViewModel(IDialogService dialogService) : base(dialogService)
    {
    }

    #region Fields

    [ObservableProperty]
    private string _user;

    [ObservableProperty]
    private string _pwd;

    #endregion

    #region Commands

    protected override async Task Submission()
    {
        DialogService.ShowBusy();
        await Task.Delay(3000);
        DialogService.CloseBusy();
    }

    #endregion
}
