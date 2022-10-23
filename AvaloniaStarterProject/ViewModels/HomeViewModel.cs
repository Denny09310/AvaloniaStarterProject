using AvaloniaStarterProject.ViewModels.Base;
using ReactiveUI;
using System.Windows.Input;

namespace AvaloniaStarterProject.ViewModels;

public class HomeViewModel : RoutableViewModelBase
{
    public static string Greeting => "Welcome to Avalonia!";

    #region Commands

    public ICommand GoToSettingsCommand
        => ReactiveCommand.Create(GoToSettings);

    #endregion

    private void GoToSettings() => _navigationService.NavigateTo<SettingsViewModel>();
}
