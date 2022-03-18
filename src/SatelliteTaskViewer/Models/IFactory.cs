using System;
using System.Collections.Generic;
using System.IO;
using GlmSharp;
using SatelliteTaskViewer.Models.Data;
using SatelliteTaskViewer.Models.Renderer;
using SatelliteTaskViewer.Models.Scene;
using SatelliteTaskViewer.ViewModels.Containers;
using SatelliteTaskViewer.ViewModels.Data;
using SatelliteTaskViewer.ViewModels.Entities;
using SatelliteTaskViewer.ViewModels.Editors;

namespace SatelliteTaskViewer.Models
{
    public interface IFactory
    {
        IdentityState CreateIdentityState();

        Spacebox CreateSpacebox(FrameViewModel parent);

        Sun CreateSun(SunData data, FrameViewModel parent);

        Earth CreateEarth(EarthData data, FrameViewModel parent);

        EntityList CreateGroundObjects(ScenarioData data, FrameViewModel parent);

        EntityList CreateGroundStations(ScenarioData data, FrameViewModel parent);

        EntityList CreateRetranslators(ScenarioData data, FrameViewModel parent);

        IList<Satellite> CreateSatellites(ScenarioData data, FrameViewModel parent, EntityList gss, EntityList rtrs);

        IList<(string Name, Satellite Satellite, IList<BaseSatelliteEvent> Events)> CreateSatelliteTasks(IList<Satellite> satellites, ScenarioData data);

        GroundObjectList CreateGroundObjectList(EntityList gos);

        ICache<TKey, TValue> CreateCache<TKey, TValue>(Action<TValue>? dispose = null);

        ISceneState CreateSceneState();

        ICamera CreateArcballCamera(dvec3 eye);

        ProjectContainerViewModel CreateProjectContainer(string name = "Project");

        ScenarioContainerViewModel CreateScenarioContainer(string name, DateTime begin, TimeSpan duration);

        //LogicalCollectionViewModel CreateLogicalCollection(string name);

        SceneTimerEditorViewModel CreateSceneTimerEditor(DateTime dateTime, TimeSpan timeSpan);
        OutlinerEditorViewModel CreateOutlinerEditor(ScenarioContainerViewModel scenario);
        IDataUpdater CreateDataUpdater();

        void SaveProjectContainer(ProjectContainerViewModel project, string path, IFileSystem fileIO, IJsonSerializer serializer);

        ProjectContainerViewModel OpenProjectContainer(string path, IFileSystem fileIO, IJsonSerializer serializer);

        ProjectContainerViewModel OpenProjectContainer(Stream stream, IFileSystem fileIO, IJsonSerializer serializer);

        void SaveProjectContainer(ProjectContainerViewModel project/*, IImageCache imageCache*/, Stream stream, IFileSystem fileIO, IJsonSerializer serializer);

        FrameViewModel CreateRootFrame();
    }
}
