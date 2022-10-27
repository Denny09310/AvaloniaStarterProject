using Avalonia.ReactiveUI;
using AvaloniaStarterProject.ViewModels;

namespace AvaloniaStarterProject.Views
{
    public partial class CounterView : ReactiveUserControl<CounterViewModel>
    {
        public CounterView()
        {
            InitializeComponent();
        }
    }
}
