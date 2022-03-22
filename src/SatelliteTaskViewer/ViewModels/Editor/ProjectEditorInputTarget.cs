using SatelliteTaskViewer.Input;

namespace SatelliteTaskViewer.ViewModels.Editor
{
    public class ProjectEditorInputTarget : IInputTarget
    {
        private readonly MainViewModel _editor;

        public ProjectEditorInputTarget(MainViewModel editor)
        {
            _editor = editor;
        }

        public void LeftDown(InputArgs args) => _editor?.CurrentTool?.LeftDown(args);

        public void LeftUp(InputArgs args) => _editor?.CurrentTool?.LeftUp(args);

        public void RightDown(InputArgs args) => _editor?.CurrentTool?.RightDown(args);

        public void RightUp(InputArgs args) => _editor?.CurrentTool?.RightUp(args);

        public void Move(InputArgs args) => _editor?.CurrentTool?.Move(args);

        public bool IsLeftDownAvailable()
        {
            return _editor?.Scenario != null;
                //&& _editor.Project.CurrentScenario.IsVisible;
        }

        public bool IsLeftUpAvailable()
        {
            return _editor?.Scenario != null;
                //&& _editor.Project.CurrentScenario.IsVisible;
        }

        public bool IsRightDownAvailable()
        {
            return _editor?.Scenario != null;
                //&& _editor.Project.CurrentScenario.IsVisible;
        }

        public bool IsRightUpAvailable()
        {
            return _editor?.Scenario != null;
                //&& _editor.Project.CurrentScenario.IsVisible;
        }

        public bool IsMoveAvailable()
        {
            return _editor.Scenario != null;
                //&& _editor.Project.CurrentScenario.IsVisible;
        }
    }
}
