using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using SatelliteTaskViewer.ViewModels.Editors;
using System.Reactive.Disposables;

namespace SatelliteTaskViewer.Avalonia.Views.Editors
{
    public partial class OutlinerEditor : ReactiveUserControl<OutlinerEditorViewModel>
    {
        private ComboBox HeaderComboBox => this.FindControl<ComboBox>("HeaderComboBox");

        public OutlinerEditor()
        {
            InitializeComponent();

            this.WhenActivated(disposables =>
            {
                this.OneWayBind(ViewModel, vm => vm.DisplayModes, v => v.HeaderComboBox.Items).DisposeWith(disposables);
                this.Bind(ViewModel, vm => vm.SelectedMode, v => v.HeaderComboBox.SelectedItem).DisposeWith(disposables);
            });
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
