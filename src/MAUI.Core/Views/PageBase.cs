namespace MAUI.Core.Views;
public class PageBase : ContentPage, IPageBase, IAnimatedPage
{
    public PageBase(IViewModelBase vm)
    {
        BindingContext = vm;
    }

    protected virtual void OnDispose()
    { 
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        (BindingContext as IViewModelBase).OnAppearing();
    }

    protected override void OnDisappearing()
    {
        (BindingContext as IViewModelBase).OnDisappearing();
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
