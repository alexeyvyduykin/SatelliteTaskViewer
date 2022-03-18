using System.Collections.Immutable;
using GlmSharp;
using SatelliteTaskViewer.Models.Data;
using SatelliteTaskViewer.Models.Entities;
using SatelliteTaskViewer.ViewModels.Entities;
using ReactiveUI.Fody.Helpers;

namespace SatelliteTaskViewer.ViewModels.Data
{
    public class AntennaAnimator : BaseState, IAnimator, IAssetable<BaseEntity>
    {
        private readonly AntennaData _data;
        private readonly IEventList<AntennaInterval> _translationEvents;
        private bool _first = true;

        public AntennaAnimator(AntennaData data)
        {
            _data = data;
            _translationEvents = new EventList<AntennaInterval>();
            Target = string.Empty;
        }

        [Reactive]
        public bool Enable { get; protected set; }

        [Reactive]
        public string Target { get; protected set; }

        [Reactive]
        public dvec3 TargetPosition { get; protected set; }

        [Reactive]
        public ImmutableArray<BaseEntity> Assets { get; set; }

        [Reactive]
        public dvec3 AttachPosition { get; set; }

        private void Init()
        {
            foreach (var rec in _data.Translations)
            {
                var begin = rec.BeginTime;
                var end = rec.EndTime;
                var target = rec.Target;

                foreach (var item in Assets)
                {
                    if (item is GroundStation gs)
                    {
                        if (gs.Name.Equals(target) == true)
                        {
                            _translationEvents.Add(new AntennaInterval(begin, end, target, gs.Frame.State));
                            break;
                        }
                    }
                    else if (item is Retranslator retranslator)
                    {
                        if (retranslator.Name.Equals(target) == true)
                        {
                            _translationEvents.Add(new AntennaInterval(begin, end, target, retranslator.Frame.State));
                        }
                    }
                }
            }


            ModelMatrix = dmat4.Translate(AttachPosition);

            _first = false;
        }

        public void Animate(double t)
        {
            if (_first == true)
            {
                Init();
            }

            var activeInterval = _translationEvents.ActiveInterval(t);

            Enable = activeInterval != default;

            if (activeInterval != default)
            {
                var value = activeInterval.Animate(t);

                Target = value.Target;
                TargetPosition = value.Position;
            }
        }
    }
}
