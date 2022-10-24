using ReactiveUI;
using System;

namespace AvaloniaStarterProject.Services.Contracts
{
    public interface INavigationService
    {
        RoutingState? Router { get; }

        event EventHandler<RoutingState>? RouterChanged;

        void NavigateBack();

        void NavigateTo<T>() where T : IRoutableViewModel;

        void NavigateToRoute(Type viewModel);

        void RegisterRouter(IScreen screen);
    }
}