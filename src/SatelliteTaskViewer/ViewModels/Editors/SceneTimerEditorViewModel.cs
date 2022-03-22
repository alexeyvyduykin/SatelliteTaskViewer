using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using SatelliteTaskViewer.Timer;
using System;
using System.Reactive;
using System.Reactive.Linq;

namespace SatelliteTaskViewer.ViewModels.Editors
{
    public class SceneTimerEditorViewModel : ViewModelBase
    {
        private double _sliderValuetemp;

        public SceneTimerEditorViewModel(ITimer timer, DateTime begin, TimeSpan duration)
        {
            Timer = timer;
            Begin = begin;
            Duration = duration;
            CurrentTime = 0.0;
            CurrentDateTime = begin;
            TimelineCurrentTime = begin;

            // 1000 milliseconds = 1 sec
            var obs = Observable.Interval(TimeSpan.FromMilliseconds(1000.0 / 60.0), RxApp.MainThreadScheduler);

            obs.Subscribe(_ =>
            {
                CurrentTime = Timer.CurrentTime;
            });

            this.WhenAnyValue(s => s.SliderValue).Subscribe(value =>
            {
                if (_sliderValuetemp != value)
                {
                    var t = value * Duration.TotalSeconds / (SliderMax - SliderMin);

                    Timer.SetTime(t);
                }
            });

            this.WhenAnyValue(s => s.CurrentTime).Subscribe(time =>
            {
                _sliderValuetemp = (int)(time * (SliderMax - SliderMin) / Duration.TotalSeconds);

                SliderValue = _sliderValuetemp;

                CurrentDateTime = Begin.AddSeconds(time);

                TimelineCurrentTime = Begin.AddSeconds(time);
            });

            Reset = ReactiveCommand.Create(ResetImpl);
            Play = ReactiveCommand.Create(PlayImpl);
            Pause = ReactiveCommand.Create(PauseImpl);
            Faster = ReactiveCommand.Create(FasterImpl);
            Slower = ReactiveCommand.Create(SlowerImpl);
        }

        [Reactive]
        public double CurrentTime { get; set; }

        [Reactive]
        public DateTime CurrentDateTime { get; private set; }

        [Reactive]
        public double SliderMin { get; private set; } = 0.0;

        [Reactive]
        public double SliderMax { get; private set; } = 1000.0;

        [Reactive]
        public double SliderValue { get; set; } = 0.0;

        [Reactive]
        public bool IsPlay { get; set; }

        [Reactive]
        public DateTime Begin { get; set; }

        [Reactive]
        public TimeSpan Duration { get; set; }

        [Reactive]
        public DateTime TimelineCurrentTime { get; private set; }

        [Reactive]
        public ITimer Timer { get; private set; }

        public ReactiveCommand<Unit, Unit> Reset { get; }
        public ReactiveCommand<Unit, Unit> Play { get; }
        public ReactiveCommand<Unit, Unit> Pause { get; }
        public ReactiveCommand<Unit, Unit> Faster { get; }
        public ReactiveCommand<Unit, Unit> Slower { get; }

        private void ResetImpl()
        {
            IsPlay = false;
            Timer.Reset();
        }

        private void PlayImpl()
        {
            if (IsPlay == false)
            {
                IsPlay = true;
                Timer.Start();
            }
        }

        private void PauseImpl()
        {
            if (IsPlay == true)
            {
                IsPlay = false;
                Timer.Pause();
            }
        }

        private void FasterImpl()
        {
            if (IsPlay == true)
            {
                if (Timer is IAcceleratedTimer acceleratedTimer)
                {
                    acceleratedTimer.Faster();
                }
            }
        }

        private void SlowerImpl()
        {
            if (IsPlay == true)
            {
                if (Timer is IAcceleratedTimer acceleratedTimer)
                {
                    acceleratedTimer.Slower();
                }
            }
        }
    }
}
