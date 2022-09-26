using System.Reactive.Disposables;

namespace MAUI.Core.ViewModels;
public class ViewModelBase : IViewModelBase
{
    public bool IsLoading { get; set; }

    protected CompositeDisposable Disposables { get; set; }

    public virtual void OnAppearing()
    {
        Disposables ??= new CompositeDisposable();
    }

    public virtual void OnDisappearing()
    {
        Disposables.Dispose();
        Disposables = null;
    }
}
