using AvaloniaStarterProject.Models;
using AvaloniaStarterProject.ViewModels.Base;
using ReactiveUI;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AvaloniaStarterProject.ViewModels
{
    public class MainViewModel : ViewModelBase, IScreen
    {
        public RoutingState Router { get; } = new();

        public IEnumerable<NavigationModel> Routes { get; } = new NavigationModel[]
        {
            new() { Icon = "fa-home", Header = "Home", ViewModel = typeof(HomeViewModel) },
            new() { Icon = "fa-cog", Header = "Settings", ViewModel = typeof(SettingsViewModel) },
        };

        protected override Task HandleActivation()
        {
            base.HandleActivation();

            _navigationService.RegisterRouter(this);
            _navigationService.NavigateTo<HomeViewModel>();

            return Task.CompletedTask;
        }
    }
}
