
using ReactiveUI;
using System.Reactive.Disposables;

namespace MAUI.Core.ViewModels;
public class ViewModelBase : ReactiveObject, IViewModelBase, IQueryAttributable
{
    public ViewModelBase()
    {
    }

    private CompositeDisposable _disposables;
    public bool IsLoading { get; set; }

    protected virtual void RegisterCommands(CompositeDisposable disposables)
    { }

    protected virtual void RegisterProperties(CompositeDisposable disposables)
    { }

    public virtual void OnAppearing()
    {
        _disposables ??= new CompositeDisposable();

        RegisterProperties(_disposables);
        RegisterCommands(_disposables);
    }

    public virtual void OnDisappearing()
    {
        _disposables.Dispose();
        _disposables = null;
    }

    public virtual void ApplyQueryAttributes(IDictionary<string, object> query)
    {
    }
}
