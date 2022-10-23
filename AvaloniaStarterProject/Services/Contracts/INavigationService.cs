using ReactiveUI;
using System;

namespace AvaloniaStarterProject.Services.Contracts
{
    public interface INavigationService
    {
        void NavigateBack();
        void NavigateTo<T>() where T : IRoutableViewModel;
        void NavigateToRoute(Type viewModel);
        void RegisterRouter(IScreen screen);
    }
}