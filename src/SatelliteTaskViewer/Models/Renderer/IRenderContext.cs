using System;
using System.Collections.Generic;
using System.Text;
using SatelliteTaskViewer.ViewModels.Geometry;
using SatelliteTaskViewer.ViewModels.Scene;
using SatelliteTaskViewer.Models.Scene;
using GlmSharp;
using SatelliteTaskViewer.ViewModels.Style;
using SatelliteTaskViewer.ViewModels.Containers;

namespace SatelliteTaskViewer.Models.Renderer
{
    public interface IRenderContext
    {
        void DrawScenario(object dc, ScenarioContainerViewModel container);

        void DrawSun(object dc, SunRenderModel sun, dmat4 modelMatrix, ISceneState scene);
        
        void DrawEarth(object dc, EarthRenderModel earth, dmat4 modelMatrix, ISceneState scene);
        
        void DrawFrame(object dc, FrameRenderModel frame, dmat4 modelMatrix, ISceneState scene);

        void DrawFrameList(object dc, FrameRenderModel frame, IEnumerable<dmat4> modelMatrices, ISceneState scene);

        void DrawOrbit(object dc, OrbitRenderModel orbit, dmat4 modelMatrix, ISceneState scene);
        
        void DrawGroundStation(object dc, RenderModel groundStation, dmat4 modelMatrix, ISceneState scene);
       
        void DrawGroundStationList(object dc, RenderModel groundStation, IEnumerable<dmat4> modelMatrices, ISceneState scene);

        void DrawGroundObject(object dc, GroundObjectRenderModel groundobject, dmat4 modelMatrix, ISceneState scene);
      
        void DrawGroundObjectList(object dc, GroundObjectRenderModel groundobject, IEnumerable<dmat4> modelMatrices, ISceneState scene);

        void DrawRetranslator(object dc, RenderModel retranslator, dmat4 modelMatrix, ISceneState scene);
        
        void DrawSatellite(object dc, RenderModel satellite, dmat4 modelMatrix, ISceneState scene);
        
        void DrawSensor(object dc, SensorRenderModel sensor, dmat4 modelMatrix, ISceneState scene);

        void DrawAntenna(object dc, AntennaRenderModel antenna, dmat4 modelMatrix, ISceneState scene);

        void DrawSpacebox(object dc, SpaceboxRenderModel spacebox, dmat4 modelMatrix, ISceneState scene);
    }
}
