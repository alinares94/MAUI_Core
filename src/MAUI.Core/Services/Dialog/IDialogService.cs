namespace MAUI.Core.Services.Dialog;
public interface IDialogService
{
    void ShowBusy();
    void CloseBusy();
    Task ShowDialogAsync(string title, string message, string close);
    Task ShowErrorAsync(Exception exception);
    Task<bool> ShowDialogConfirmationAsync(string title, string message, string cancel, string ok);
    Task<string> DisplayActionSheet(string title, string cancel, string[] buttons);
}
