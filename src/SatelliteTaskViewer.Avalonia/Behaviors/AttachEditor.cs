using Avalonia;
using Avalonia.Controls;
using SatelliteTaskViewer.Avalonia.Editor;
using SatelliteTaskViewer.Input;
using SatelliteTaskViewer.Models.Editor;
using SatelliteTaskViewer.ViewModels.Editor;
using System;

namespace SatelliteTaskViewer.Avalonia.Behaviors
{
    public class AttachEditor
    {
        private readonly Control _control;
        private AvaloniaInputSource__? _inputSource;
        private ProjectEditorInputTarget? _inputTarget;
        private InputProcessor? _inputProcessor;

        public AttachEditor(Control control)
        {
            _control = control;
            _control.GetObservable(Control.DataContextProperty).Subscribe(Changed);
        }

        public void InvalidateChild(double zoomX, double zoomY, double offsetX, double offsetY)
        {
            if (!(_control.DataContext is MainViewModel mainViewModel))
            {
                return;
            }

            //var state = projectEditor.PageState;
            //if (state != null)
            //{
            //    state.ZoomX = zoomX;
            //    state.ZoomY = zoomY;
            //    state.PanX = offsetX;
            //    state.PanY = offsetY;
            //}
        }

        public void Changed(object? context)
        {
            Detach();
            Attach();
        }

        public void Attach()
        {
            if (!(_control.DataContext is MainViewModel mainViewModel))
            {
                return;
            }

            var presenterControlEditor = _control.Find<Control>("presenterControlEditor");
            //var zoomBorder = _control.Find<ZoomBorder>("zoomBorder");

            if (mainViewModel.CanvasPlatform is IEditorCanvasPlatform canvasPlatform)
            {
                canvasPlatform.InvalidateControl = () =>
                {
                    presenterControlEditor?.InvalidateVisual();
                };
                //canvasPlatform.ResetZoom = () => zoomBorder?.Reset();
                //canvasPlatform.FillZoom = () => zoomBorder?.Fill();
                //canvasPlatform.UniformZoom = () => zoomBorder?.Uniform();
                //canvasPlatform.UniformToFillZoom = () => zoomBorder?.UniformToFill();
                //canvasPlatform.AutoFitZoom = () => zoomBorder?.AutoFit();
                //canvasPlatform.InZoom = () => zoomBorder?.ZoomIn();
                //canvasPlatform.OutZoom = () => zoomBorder?.ZoomOut();
                //canvasPlatform.Zoom = zoomBorder;
            }

            //if (zoomBorder != null)
            //{
            //    zoomBorder.InvalidatedChild = InvalidateChild;
            //}

            // _inputSource = new AvaloniaInputSource(zoomBorder, presenterControlEditor, p => p);
            _inputSource = new AvaloniaInputSource__(presenterControlEditor);
            _inputTarget = new ProjectEditorInputTarget(mainViewModel);
            _inputProcessor = new InputProcessor();
            _inputProcessor.Connect(_inputSource, _inputTarget);
        }

        public void Detach()
        {
            if (!(_control.DataContext is MainViewModel mainViewModel))
            {
                return;
            }

            //var zoomBorder = _control.Find<ZoomBorder>("zoomBorder");

            //if (projectEditor.CanvasPlatform is IEditorCanvasPlatform canvasPlatform)
            //{
            //    canvasPlatform.InvalidateControl = null;
            //    canvasPlatform.ResetZoom = null;
            //    canvasPlatform.FillZoom = null;
            //    canvasPlatform.UniformZoom = null;
            //    canvasPlatform.UniformToFillZoom = null;
            //    canvasPlatform.AutoFitZoom = null;
            //    canvasPlatform.InZoom = null;
            //    canvasPlatform.OutZoom = null;
            //    canvasPlatform.Zoom = null;
            //}

            //if (zoomBorder != null)
            //{
            //    zoomBorder.InvalidatedChild = null;
            //}

            _inputProcessor?.Dispose();
            _inputProcessor = null;
            _inputTarget = null;
            _inputSource = null;
        }
    }
}
