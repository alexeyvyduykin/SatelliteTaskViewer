using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using SatelliteTaskViewer.ViewModels.Editor;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace SatelliteTaskViewer.Avalonia.Views
{
    public partial class DashboardView : ReactiveUserControl<MainViewModel>
    {
        private Button JsonButton => this.FindControl<Button>("JsonButton");
        private Button DatabaseButton => this.FindControl<Button>("DatabaseButton");

        public DashboardView()
        {
            InitializeComponent();

            this.WhenActivated(disposables =>
            {
                JsonButton.Events().Click.Select(_ => Unit.Default).InvokeCommand(this, x => x.ViewModel!.FromJson).DisposeWith(disposables);
                DatabaseButton.Events().Click.Select(_ => Unit.Default).InvokeCommand(this, x => x.ViewModel!.FromDatabase).DisposeWith(disposables);
            });
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
