using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MAUI.Core.ViewModels;
public class ViewModelBase : IViewModelBase, IQueryAttributable
{
    public ViewModelBase(IDialogService dialogService)
    {
        DialogService = dialogService;
        RegisterCommands();
    }

    #region Properties

    protected IDialogService DialogService { get; init; }

    #endregion

    #region Methods

    protected virtual void RegisterCommands()
    { 
    }

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

    #region INotifyPropertyChanged

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    #endregion
}
