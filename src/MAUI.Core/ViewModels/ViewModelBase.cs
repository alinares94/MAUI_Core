namespace MAUI.Core.ViewModels;
public partial class ViewModelBase : ObservableValidator, IViewModelBase, IQueryAttributable
{
    public ViewModelBase(IDialogService dialogService)
    {
        DialogService = dialogService;
    }

    #region Properties

    protected IDialogService DialogService { get; init; }

    #endregion

    #region Methods

    public virtual void ApplyQueryAttributes(IDictionary<string, object> query)
    {
    }

    public virtual void OnAppearing()
    {
    }

    public virtual void OnDisappearing()
    {
    }

    public virtual Task PageLoadedAsync(IEnumerable<NavigationParam> @params)
    {
        return Task.CompletedTask;
    }

    #endregion
}
