using Avalonia.ReactiveUI;
using AvaloniaStarterProject.ViewModels;

namespace AvaloniaStarterProject.Windows
{
    public partial class MainWindow : ReactiveWindow<MainViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}