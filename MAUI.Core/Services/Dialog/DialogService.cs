namespace MAUI.Core.Services.Dialog;
internal class DialogService : IDialogService
{
    public async Task ShowDialogAsync(string title, string message, string close)
    {
        await Application.Current.MainPage.DisplayAlert(title, message, close);
    }

    public async Task ShowErrorAsync(Exception exception)
    {
        await ShowDialogAsync("Error", exception.Message, "Cerrar");
    }

    public async Task<bool> ShowDialogConfirmationAsync(string title, string message, string cancel, string ok)
    {
        return await Application.Current.MainPage.DisplayAlert(title, message, ok, cancel);
    }

    public async Task<string> DisplayActionSheet(string title, string cancel, string[] buttons)
    {
        return await Application.Current.MainPage.DisplayActionSheet(title, cancel, null, buttons);
    }
}
