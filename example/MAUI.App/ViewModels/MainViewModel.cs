using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace MAUI.App.ViewModels;
public class MainViewModel : ViewModelBase
{
    public MainViewModel(IDialogService dialogService) : base(dialogService)
    {
    }

    #region Fields

    private string _user;
    private string _pwd;

    #endregion

    #region Properties

    public string User
    {
        get => _user;
        set => SetField(ref _user, value);
    }

    public string Pwd
    {
        get => _pwd;
        set => SetField(ref _pwd, value);
    }

    #endregion

    #region Commands

    public ICommand SubmitCommand { get; set; }

    protected override void RegisterCommands()
    {
        base.RegisterCommands();

        SubmitCommand = new AsyncRelayCommand(Submit);
    }

    #endregion

    #region Methods

    private async Task Submit()
    {
        DialogService.ShowBusy();
        await Task.Delay(3000);
        DialogService.CloseBusy();
    }

    #endregion
}
