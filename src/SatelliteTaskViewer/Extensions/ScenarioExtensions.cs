using SatelliteTaskViewer.ViewModels;
using SatelliteTaskViewer.ViewModels.Entities;
using System.Collections.Generic;

namespace SatelliteTaskViewer
{
    public static class ScenarioExtensions
    {
        public static T AddEntity<T>(this Scenario scenario, T entity) where T : notnull, BaseEntity
        {
            if (scenario.OutlinerEditor != null)
            {
                var builder = scenario.OutlinerEditor.Entities.ToBuilder();
                builder.Add(entity);
                scenario.OutlinerEditor.Entities = builder.ToImmutable();
            }

            return entity;
        }

        public static IList<T> AddEntities<T>(this Scenario scenario, IList<T> entities) where T : notnull, BaseEntity
        {
            if (scenario.OutlinerEditor != null)
            {
                var builder = scenario.OutlinerEditor.Entities.ToBuilder();
                builder.AddRange(entities);
                scenario.OutlinerEditor.Entities = builder.ToImmutable();
            }

            return entities;
        }

        public static void AddGroundObjectList(this Scenario scenario, GroundObjectList list)
        {
            scenario.GroundObjectList = list;
        }
    }
}
