﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SatelliteTaskViewer.Timer
{
    public interface ITimer
    {
        void Start();
        void Pause();
        void Reset();
        void SetTime(double t);

        double CurrentTime { get; }
        bool IsRunning { get; }
    }
}
