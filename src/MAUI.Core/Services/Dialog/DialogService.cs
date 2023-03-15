using CommunityToolkit.Maui.Views;
using MAUI.Core.Controls;

namespace MAUI.Core.Services.Dialog;
public class DialogService : IDialogService
{
    private Popup _busyPopup = new BusyControl();
    private int _busyCount;

    public async Task ShowDialogAsync(string title, string message, string close)
    {
        await Application.Current.MainPage!.DisplayAlert(title, message, close);
    }

    public async Task ShowErrorAsync(Exception exception)
    {
        await ShowDialogAsync("Error", exception.Message, "Cerrar");
    }

    public async Task<bool> ShowDialogConfirmationAsync(string title, string message, string cancel, string ok)
    {
        return await Application.Current.MainPage!.DisplayAlert(title, message, ok, cancel);
    }

    public async Task<string> DisplayActionSheet(string title, string cancel, string[] buttons)
    {
        return await Application.Current.MainPage!.DisplayActionSheet(title, cancel, null, buttons);
    }

    public void ShowBusy()
    {
        if (_busyCount > 0)
            return;

        _busyCount++;
        _busyPopup = new BusyControl();
        Application.Current.MainPage.ShowPopup(_busyPopup);
    }

    public void CloseBusy()
    {
        _busyCount--;

        if (_busyCount.Equals(0))
            _busyPopup.Close();
    }
}
