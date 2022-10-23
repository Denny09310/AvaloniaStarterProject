using AvaloniaStarterProject.Helpers;
using AvaloniaStarterProject.Services.Contracts;
using ReactiveUI;
using System;
using System.Reflection;

namespace AvaloniaStarterProject.Services;

public class NavigationService : INavigationService
{
    private string? _currentRouteName;
    private RoutingState? _router;

    public void RegisterRouter(IScreen screen) => _router = screen.Router;

    public void NavigateTo<T>() where T : IRoutableViewModel
    {
        if (_router is null)
            throw new InvalidOperationException("You must register the router before navigating");

        var route = Resolver.GetService<T>();
        if (route is null)
            throw new InvalidOperationException($"The route {typeof(T).Name} is not registered in the DI Container");

        if (route.UrlPathSegment == _currentRouteName) return;

        _router.Navigate.Execute(route)
                        .Subscribe(x => _currentRouteName = x.UrlPathSegment);
    }

    public void NavigateToRoute(Type viewModel)
        => GetType().GetMethod(nameof(NavigateTo))?
                    .MakeGenericMethod(viewModel)
                    .Invoke(this, null);

    public void NavigateBack()
        => _router?.NavigateBack.Execute()
            .Subscribe(x => _currentRouteName = x?.UrlPathSegment);
}
