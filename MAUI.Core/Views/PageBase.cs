using ReactiveUI;
using ReactiveUI.Maui;
using System.Reactive.Disposables;

namespace MAUI.Core.Views;

public class PageBase<TViewModel> : ReactiveContentPage<TViewModel>, IPageBase, IAnimatedPage
    where TViewModel : class, IViewModelBase
{
    public PageBase(TViewModel vm)
    {
        ViewModel = vm;
        this.WhenActivated(disposables =>
        {
            OnActivated(disposables);
            Disposable.Create(() => OnDispose()).DisposeWith(disposables);
        });
    }

    protected virtual void OnDispose()
    { }

    protected virtual void OnActivated(CompositeDisposable disposables)
    { }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ViewModel.OnAppearing();
    }

    protected override void OnDisappearing()
    {
        ViewModel.OnDisappearing();
        base.OnDisappearing();
    }

    public Task RunDisappearingAnimationAsync()
    {
        return Task.CompletedTask;
    }

    public Task RunAppearingAnimationAsync()
    {
        return Task.CompletedTask;
    }
}
