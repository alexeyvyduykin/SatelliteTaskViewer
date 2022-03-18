using System;
using System.Reactive.Linq;
using SatelliteTaskViewer.Timer;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace SatelliteTaskViewer.ViewModels.Editors
{
    public enum TimerMode { Play, Stop, Pause };

    public delegate void TimeEventHandler(object? sender, TimeEventArgs e);

    public class TimeEventArgs : EventArgs
    {
        private readonly double _time;
        public TimeEventArgs(double time)
        {
            _time = time;
        }

        public double Time => _time;
    }

    public class SceneTimerEditorViewModel : ViewModelBase
    {
        private readonly System.Timers.Timer _timerThread;
        private double _sliderValuetemp;

        public SceneTimerEditorViewModel(ITimer timer, DateTime begin, TimeSpan duration)
        {
            Timer = timer;
            Begin = begin;
            Duration = duration;
            CurrentTime = 0.0;
            CurrentDateTime = begin;
            TimelineCurrentTime = begin;

            TimerMode = TimerMode.Stop;

            // 1000 milliseconds = 1 sec
            _timerThread = new System.Timers.Timer(1000.0 / 60.0);

            _timerThread.Elapsed += TimerThreadElapsed;

            _timerThread.AutoReset = true;
            _timerThread.Enabled = true;

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
                _sliderValuetemp = (int)(CurrentTime * (SliderMax - SliderMin) / Duration.TotalSeconds);

                SliderValue = _sliderValuetemp;
            });
        }

        private void TimerThreadElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            CurrentTime = Timer.CurrentTime;

            TimelineCurrentTime = Begin.AddSeconds(CurrentTime);

            CurrentDateTime = Begin.AddSeconds(CurrentTime);
        }

        [Reactive]
        public DateTime CurrentDateTime { get; set; }

        [Reactive]
        public double SliderMin { get; protected set; } = 0.0;

        [Reactive]
        public double SliderMax { get; protected set; } = 1000.0;

        [Reactive]
        public double SliderValue { get; set; } = 0.0;

        [Reactive]
        public TimerMode TimerMode { get; set; }

        [Reactive]
        public DateTime Begin { get; set; }

        [Reactive]
        public TimeSpan Duration { get; set; }

        [Reactive]
        public double CurrentTime { get; protected set; }

        [Reactive]
        public DateTime TimelineCurrentTime { get; protected set; }

        [Reactive]
        public ITimer Timer { get; protected set; }

        public void OnReset()
        {
            TimerMode = TimerMode.Stop;
            Timer.Reset();
        }

        public void OnPlay()
        {
            if (TimerMode == TimerMode.Stop || TimerMode == TimerMode.Pause)
            {
                TimerMode = TimerMode.Play;
                Timer.Start();
            }
        }

        public void OnPause()
        {
            if (TimerMode == TimerMode.Play)
            {
                TimerMode = TimerMode.Pause;
                Timer.Pause();
            }
        }

        public void OnFaster()
        {
            if (TimerMode == TimerMode.Play)
            {
                if (Timer is IAcceleratedTimer acceleratedTimer)
                {
                    acceleratedTimer.Faster();
                }
            }
        }

        public void OnSlower()
        {
            if (TimerMode == TimerMode.Play)
            {
                if (Timer is IAcceleratedTimer acceleratedTimer)
                {
                    acceleratedTimer.Slower();
                }
            }
        }
    }
}
