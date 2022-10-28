using AvaloniaStarterProject.ViewModels.Base;
using ReactiveUI;

namespace AvaloniaStarterProject.ViewModels;

[ReactiveObject]
public partial class HomeViewModel : RoutableViewModelBase
{
    public static string Greeting => "Welcome to Avalonia!";

    [ReactiveCommand]
    private void GoToSettings() => _navigationService.NavigateTo<SettingsViewModel>();
}