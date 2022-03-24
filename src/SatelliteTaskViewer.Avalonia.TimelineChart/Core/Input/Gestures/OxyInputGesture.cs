using System;

namespace TimelineChart.Core
{
    public abstract class OxyInputGesture : IEquatable<OxyInputGesture>
    {
        public abstract bool Equals(OxyInputGesture? other);
    }
}
