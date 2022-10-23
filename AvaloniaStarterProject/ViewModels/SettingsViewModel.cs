using AvaloniaStarterProject.ViewModels.Base;
using ReactiveUI;
using System.Windows.Input;

namespace AvaloniaStarterProject.ViewModels;

public class SettingsViewModel : RoutableViewModelBase
{
	#region Commands

	public ICommand GoToHomeCommand
		=> ReactiveCommand.Create(GoToHome);

	#endregion

	private void GoToHome() => _navigationService.NavigateBack();
}
