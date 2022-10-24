using AvaloniaStarterProject.Helpers;
using AvaloniaStarterProject.Services.Contracts;
using ReactiveUI;
using System;

namespace AvaloniaStarterProject.Services;

public class NavigationService : INavigationService
{
    private string? _currentRouteName;

    public event EventHandler<RoutingState>? RouterChanged;

    public RoutingState? Router { get; private set; }

    public void NavigateBack()
        => Router?.NavigateBack.Execute()
            .Subscribe(x => _currentRouteName = x?.UrlPathSegment);

    public void NavigateTo<T>() where T : IRoutableViewModel
    {
        if (Router is null)
            throw new InvalidOperationException("You must register the router before navigating");

        var route = Resolver.GetService<T>();
        if (route is null)
            throw new InvalidOperationException($"The route {typeof(T).Name} is not registered in the DI Container");

        if (route.UrlPathSegment == _currentRouteName) return;

        Router.Navigate.Execute(route)
                        .Subscribe(x => _currentRouteName = x.UrlPathSegment);
    }

    public void NavigateToRoute(Type viewModel)
        => GetType().GetMethod(nameof(NavigateTo))?
                    .MakeGenericMethod(viewModel)
                    .Invoke(this, null);

    public void RegisterRouter(IScreen screen)
    {
        Router = screen.Router;
        RouterChanged?.Invoke(this, Router);
    }
}