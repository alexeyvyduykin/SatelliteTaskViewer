using System;
using System.Linq;
using GlmSharp;
using SatelliteTaskViewer.Models;
using SatelliteTaskViewer.Models.Data;
using SatelliteTaskViewer.Models.Scene;
using SatelliteTaskViewer.ViewModels.Editors;
using SatelliteTaskViewer.ViewModels.Entities;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using SatelliteTaskViewer.ViewModels.Containers;

namespace SatelliteTaskViewer.ViewModels
{
    public class Scenario : BaseContainerViewModel
    {
        public Scenario()
        {
            this.WhenAnyValue(s => s.Width, s => s.Height, (w, h) => (w, h)).Subscribe(bounds =>
            {
                if (SceneState != null && SceneState.Camera is IArcballCamera arcballCamera)
                {
                    var (w, h) = bounds;
                    arcballCamera.Resize((int)w, (int)h);
                }
            });

            this.WhenAnyValue(s => s.TaskListEditor!.CurrentTask).Subscribe(task =>
            {
                if (task == null)
                {
                    return;
                }

                SetCameraTo(task.Satellite);
            });
        }

        [Reactive]
        public IDataUpdater? Updater { get; set; }

        [Reactive]
        public GroundObjectList? GroundObjectList { get; set; }

        [Reactive]
        public ISceneState? SceneState { get; set; }

        [Reactive]
        public double Width { get; set; }

        [Reactive]
        public double Height { get; set; }

        [Reactive]
        public SceneTimerEditorViewModel? SceneTimerEditor { get; set; }

        [Reactive]
        public TaskListEditorViewModel? TaskListEditor { get; set; }

        [Reactive]
        public OutlinerEditorViewModel? OutlinerEditor { get; set; }

        [Reactive]
        public SceneInfoTab? SceneInfoTab { get; set; }

        public void SetCameraTo(ITargetable target)
        {
            if (SceneState == null)
            {
                return;
            }

            var behaviours = SceneState.CameraBehaviours;
            var targetType = target.GetType();

            if (behaviours == null || SceneState.Target == null || SceneState.Camera == null)
            {
                return;
            }

            if (behaviours.ContainsKey(targetType))
            {
                // save behaviour for current target type
                var (_, func) = behaviours[SceneState.Target.GetType()];
                behaviours[SceneState.Target.GetType()] = (SceneState.Camera.Eye, func);

                var newBehaviour = behaviours[targetType];
                SceneState.Camera.LookAt(newBehaviour.eye, dvec3.Zero, dvec3.UnitY);
                SceneState.Target = target;
            }
        }

        public void LogicalUpdate()
        {         
            if (OutlinerEditor == null || SceneTimerEditor == null)
            {
                return;
            }

            //if (TimePresenter.Timer.IsRunning == true)
            {
                var root = OutlinerEditor.FrameRoot.Single();
                Updater?.Update(SceneTimerEditor.Timer.CurrentTime, root);
            }
        }

        public int AbsolutePositionX { get; set; }

        public int AbsolutePositionY { get; set; }

        public int ZIndex { get; set; }
    }
}
