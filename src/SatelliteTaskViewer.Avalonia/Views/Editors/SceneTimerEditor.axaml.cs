using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using SatelliteTaskViewer.ViewModels.Editors;
using System;
using System.Globalization;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace SatelliteTaskViewer.Avalonia.Views.Editors
{
    public partial class SceneTimerEditor : ReactiveUserControl<SceneTimerEditorViewModel>
    {
        private TextBlock DateTextBlock => this.FindControl<TextBlock>("DateTextBlock");
        private TextBlock TimeTextBlock => this.FindControl<TextBlock>("TimeTextBlock");
        private Slider TimerSlider => this.FindControl<Slider>("TimerSlider");
        private Button ResetToBeginButton => this.FindControl<Button>("ResetToBeginButton");
        private Button PlayBackButton => this.FindControl<Button>("PlayBackButton");
        private Button PlayForwardButton => this.FindControl<Button>("PlayForwardButton");
        private Button PauseButton => this.FindControl<Button>("PauseButton");
        private Button ResetToEndButton => this.FindControl<Button>("ResetToEndButton");
        private Button SlowerButton => this.FindControl<Button>("SlowerButton");
        private Button FasterButton => this.FindControl<Button>("FasterButton");

        public SceneTimerEditor()
        {
            InitializeComponent();

            this.WhenActivated(disposables =>
            {
                this.WhenAnyValue(x => x.DataContext).BindTo(this, x => x.ViewModel).DisposeWith(disposables);

                // Slider
                this.OneWayBind(ViewModel, vm => vm.SliderMin, v => v.TimerSlider.Minimum).DisposeWith(disposables);
                this.OneWayBind(ViewModel, vm => vm.SliderMax, v => v.TimerSlider.Maximum).DisposeWith(disposables);
                this.Bind(ViewModel, vm => vm.SliderValue, v => v.TimerSlider.Value).DisposeWith(disposables);

                ResetToBeginButton.Events().Click.Select(_ => Unit.Default).InvokeCommand(this, x => x.ViewModel!.Reset).DisposeWith(disposables);
                PlayBackButton.Events().Click.Select(_ => Unit.Default).InvokeCommand(this, x => x.ViewModel!.Play).DisposeWith(disposables);
                this.OneWayBind(ViewModel, vm => vm.IsPlay, v => v.PlayBackButton.IsVisible, value => !value).DisposeWith(disposables);
                PlayForwardButton.Events().Click.Select(_ => Unit.Default).InvokeCommand(this, x => x.ViewModel!.Play).DisposeWith(disposables);
                this.OneWayBind(ViewModel, vm => vm.IsPlay, v => v.PlayForwardButton.IsVisible, value => !value).DisposeWith(disposables);
                PauseButton.Events().Click.Select(_ => Unit.Default).InvokeCommand(this, x => x.ViewModel!.Pause).DisposeWith(disposables);
                this.OneWayBind(ViewModel, vm => vm.IsPlay, v => v.PauseButton.IsVisible).DisposeWith(disposables);
                ResetToEndButton.Events().Click.Select(_ => Unit.Default).InvokeCommand(this, x => x.ViewModel!.Reset).DisposeWith(disposables);
                SlowerButton.Events().Click.Select(_ => Unit.Default).InvokeCommand(this, x => x.ViewModel!.Slower).DisposeWith(disposables);
                FasterButton.Events().Click.Select(_ => Unit.Default).InvokeCommand(this, x => x.ViewModel!.Faster).DisposeWith(disposables);

                this.OneWayBind(ViewModel, vm => vm.CurrentDateTime, v => v.DateTextBlock.Text, ConvertToDate).DisposeWith(disposables);
                this.OneWayBind(ViewModel, vm => vm.CurrentDateTime, v => v.TimeTextBlock.Text, ConvertToTime).DisposeWith(disposables);
            });
        }

        private string ConvertToDate(DateTime dt)
        {
            return dt.ToString("dd-MMM-yyyy", CultureInfo.CreateSpecificCulture("en-US"));
        }

        private string ConvertToTime(DateTime dt)
        {
            return dt.ToString("HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US"));
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
