using System;
using System.Collections.Generic;
using System.Linq;
using ReactiveUI.Fody.Helpers;

namespace SatelliteTaskViewer.ViewModels.Entities
{
    public class GroundObjectList : ViewModelBase
    {
        private readonly EntityList _gos;

        public GroundObjectList(EntityList gos)
        {
            _gos = gos;

            GroundObjects = CreateFrom(_gos);
            SelectedGroundObject = GroundObjects.FirstOrDefault();
            SearchString = string.Empty;

            PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(GroundObjectList.SearchString))
                {
                    GroundObjects = CreateFrom(_gos);
                    SelectedGroundObject = GroundObjects.FirstOrDefault();
                }
            };
        }

        private IList<GroundObject> CreateFrom(EntityList source)
        {
            Func<GroundObject, bool> namePredicate =
                (s => (string.IsNullOrEmpty(SearchString) == false) ? s.Name.Contains(SearchString) : true);

            var list = source.Values.Cast<GroundObject>();

            return list.Where(namePredicate).ToList();
        }

        [Reactive]
        public string SearchString { get; set; }

        [Reactive]
        public IList<GroundObject> GroundObjects { get; set; }

        [Reactive]
        public GroundObject? SelectedGroundObject { get; set; }
    }
}
