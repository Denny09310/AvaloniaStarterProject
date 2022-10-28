using AvaloniaStarterProject.ViewModels.Base;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace AvaloniaStarterProject.ViewModels;

[ReactiveObject]
public partial class CounterViewModel : RoutableViewModelBase
{
    [Reactive]
    public int Counter { get; set; }

    [Reactive]
    public bool ChangeCounterLock { get; set; }

    private IObservable<bool> CanIncrementCounter()
        => this.WhenAnyValue(x => x.ChangeCounterLock)
               .Select(x => !x);

    [ReactiveCommand(CanExecute = nameof(CanIncrementCounter))]
    private void IncrementCounter() => Counter++;

    [ReactiveCommand(CanExecute = nameof(CanIncrementCounter))]
    private void DecrementCounter() => Counter--;

    [ReactiveCommand(CanExecute = nameof(CanIncrementCounter))]
    private async Task DecrementCounterAsync()
    {
        await Task.Delay(2500);
        Counter--;
    }
}
