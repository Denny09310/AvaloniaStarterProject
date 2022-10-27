using AvaloniaStarterProject.Services.Contracts;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Windows.Input;

namespace AvaloniaStarterProject.Models;

public partial class NavigationModel : ReactiveObject
{
    private INavigationService? _navigationService;

    public NavigationModel()
    {
        NavigateToCommand = ReactiveCommand.Create(() => _navigationService?.NavigateToRoute(ViewModel!));
    }

    public string? Header { get; set; }

    public string? Icon { get; set; }

    [Reactive]
    public bool IsSelected { get; set; }

    public ICommand NavigateToCommand { get; }

    public Type? ViewModel { get; set; }

    public void SetNavigationService(INavigationService navigationService)
    {
        _navigationService = navigationService;
        _navigationService.RouterChanged += (s, e) =>
        {
            e.Navigate.Subscribe(CheckNavigationRoute);
            e.NavigateBack.Subscribe(CheckNavigationRoute);
        };
    }

    private void CheckNavigationRoute(IRoutableViewModel? viewModel)
    {
        if (viewModel?.GetType() == ViewModel) IsSelected = true;
    }
}