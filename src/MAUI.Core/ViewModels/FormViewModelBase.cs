using CommunityToolkit.Mvvm.Input;

namespace MAUI.Core.ViewModels;
public abstract partial class FormViewModelBase : ViewModelBase, IFormViewModelBase
{
    public FormViewModelBase(IDialogService dialogService) : base(dialogService)
    {
    }

    #region Fields

    [ObservableProperty]
    private string _error;

    #endregion

    #region Commands

    [RelayCommand]
    private async Task Submit()
    {
        ValidateAllProperties();
        Error = GetError();
        if (HasErrors)
        {
            await SubmissionErrors();
            return;
        }

        await Submission();
        await EndSubmission();
    }

    #endregion

    #region Methods

    protected virtual async Task SubmissionErrors()
    {
        await DialogService.ShowDialogAsync("Error", Error, "Cerrar");
    }

    protected abstract Task Submission();

    protected virtual Task EndSubmission() => Task.CompletedTask;

    protected virtual string GetError()
    {
        return string.Join(
            Environment.NewLine, 
            GetErrors().Select(x => $"{string.Join(';', x.MemberNames)}. {x.ErrorMessage}"));
    }

    #endregion
}
