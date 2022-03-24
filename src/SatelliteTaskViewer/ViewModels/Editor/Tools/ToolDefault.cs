using SatelliteTaskViewer.Input;
using SatelliteTaskViewer.Models.Editor;
using SatelliteTaskViewer.Models.Scene;
using System;

namespace SatelliteTaskViewer.ViewModels.Editor.Tools
{
    public class ToolDefault : IEditorTool
    {
        public enum State { None, Zoom, Rotate }
        private readonly IServiceProvider _serviceProvider;
        private State _currentState = State.None;
        private (double x, double y) _lastPoint;

        public ToolDefault(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void LeftDown(InputArgs args)
        {
            var mainViewModel = _serviceProvider.GetService<MainViewModel>();

            if (mainViewModel.Scenario == null || mainViewModel.Scenario.SceneState == null)
            {
                return;
            }

            var camera = (IArcballCamera)mainViewModel.Scenario.SceneState.Camera;

            camera.RotateBegin((int)args.X, (int)args.Y);

            _currentState = State.Rotate;

            _lastPoint = (args.X, args.Y);
        }

        public void LeftUp(InputArgs args)
        {
            _currentState = State.None;

            var mainViewModel = _serviceProvider.GetService<MainViewModel>();

            if (mainViewModel.Scenario == null || mainViewModel.Scenario.SceneState == null)
            {
                return;
            }

            var camera = (IArcballCamera)mainViewModel.Scenario.SceneState.Camera;

            camera.RotateEnd((int)args.X, (int)args.Y);
        }

        public void RightDown(InputArgs args)
        {
            _currentState = State.Zoom;

            _lastPoint = (args.X, args.Y);
        }

        public void RightUp(InputArgs args)
        {
            _currentState = State.None;
        }

        public void Move(InputArgs args)
        {
            var mainViewModel = _serviceProvider.GetService<MainViewModel>();

            if (mainViewModel.Scenario == null)
            {
                return;
            }

            if (_currentState == State.Rotate && mainViewModel.Scenario.SceneState != null)
            {
                var camera = (IArcballCamera)mainViewModel.Scenario.SceneState.Camera;

                camera.Rotate((int)args.X, (int)args.Y);
            }
            else if (_currentState == State.Zoom && mainViewModel.Scenario.SceneState != null)
            {
                var sceneState = mainViewModel.Scenario.SceneState;
                var camera = (IArcballCamera)sceneState.Camera;
                var target = sceneState.Target;
                var (_, func) = sceneState.CameraBehaviours[target.GetType()];

                double value = (double)(args.Y - _lastPoint.y);

                var dz = func.Invoke(camera.Eye.Length);

                camera.Zoom(Math.Sign(value) * dz);
            }

            _lastPoint = (args.X, args.Y);
        }
    }

}
