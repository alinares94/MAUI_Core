namespace MAUI.Core.Views;
public interface IAnimatedPage
{
    Task RunDisappearingAnimationAsync();
    Task RunAppearingAnimationAsync();
}
