using AvaloniaStarterProject.Services.Contracts;
using ReactiveUI;
using System;
using System.Windows.Input;

namespace AvaloniaStarterProject.Models;

public class NavigationModel
{
    private INavigationService? _navigationService;

    public NavigationModel()
    {
        NavigateToCommand = ReactiveCommand.Create(() => _navigationService?.NavigateToRoute(ViewModel!));
    }

    public bool IsSelected { get; set; }
    public string? Header { get; set; }
    public string? Icon { get; set; }
    public Type? ViewModel { get; set; }
    public ICommand NavigateToCommand { get; }

    public void SetNavigationService(INavigationService navigationService) 
        => _navigationService = navigationService;
}
