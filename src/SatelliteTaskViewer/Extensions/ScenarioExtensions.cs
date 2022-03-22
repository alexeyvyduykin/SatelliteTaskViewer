using SatelliteTaskViewer.ViewModels.Containers;
using SatelliteTaskViewer.ViewModels.Entities;
using System.Collections.Generic;

namespace SatelliteTaskViewer
{
    public static class ScenarioExtensions
    {
        public static T AddEntity<T>(this Scenario scenario, T entity) where T : BaseEntity
        {
            if (entity != null)
            {
                var builder = scenario.OutlinerEditor.Entities.ToBuilder();
                builder.Add(entity);
                scenario.OutlinerEditor.Entities = builder.ToImmutable();
            }

            return entity;
        }

        public static IList<T> AddEntities<T>(this Scenario scenario, IList<T> entities) where T : BaseEntity
        {
            if (entities != null)
            {
                var builder = scenario.OutlinerEditor.Entities.ToBuilder();
                builder.AddRange(entities);
                scenario.OutlinerEditor.Entities = builder.ToImmutable();
            }

            return entities;
        }


        //public static void AddSatelliteTask(this ScenarioContainerViewModel scenario, SatelliteTask task)
        //{
        //    if (scenario?.TaskListEditor.Tasks != null && task != null)
        //    {
        //        var builder = scenario.TaskListEditor.Tasks.ToBuilder();
        //        builder.Add(task);
        //        scenario.TaskListEditor.Tasks = builder.ToImmutable();
        //    }
        //}

        //public static void AddSatelliteTasks(this ScenarioContainerViewModel scenario, IList<SatelliteTask> tasks)
        //{
        //    if (scenario?.TaskListEditor.Tasks != null && tasks != null)
        //    {
        //        var builder = scenario.TaskListEditor.Tasks.ToBuilder();
        //        builder.AddRange(tasks);
        //        scenario.TaskListEditor.Tasks = builder.ToImmutable();
        //    }
        //}

        //public static void AddSatelliteTask(this ScenarioContainerViewModel scenario, SatelliteTask task)
        //{
        //    if (scenario?.TaskListEditor.Tasks != null && task != null)
        //    {
        //        scenario.TaskListEditor.Tasks.Add(task);

        //        var list = scenario.TaskListEditor.Tasks;//.ToList();
        //        list.Add(task);
        //        scenario.TaskListEditor.Tasks = list;// new SourceList<SatelliteTask>(list);
        //    }
        //}

        public static void AddGroundObjectList(this Scenario scenario, GroundObjectList list)
        {
            scenario.GroundObjectList = list;
        }
    }
}
