using AvaloniaStarterProject.ViewModels.Base;
using ReactiveUI;

namespace AvaloniaStarterProject.ViewModels;

[ReactiveObject]
public partial class SettingsViewModel : RoutableViewModelBase
{
    [ReactiveCommand]
    private void GoToHome() => _navigationService.NavigateBack();
}