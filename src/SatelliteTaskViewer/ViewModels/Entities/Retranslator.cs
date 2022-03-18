﻿using SatelliteTaskViewer.Models;
using SatelliteTaskViewer.Models.Renderer;
using SatelliteTaskViewer.Models.Scene;
using SatelliteTaskViewer.ViewModels.Scene;
using ReactiveUI.Fody.Helpers;

namespace SatelliteTaskViewer.ViewModels.Entities
{
    public class Retranslator : BaseEntity, IDrawable
    {
        [Reactive]
        public FrameViewModel Frame { get; set; }

        [Reactive]
        public RenderModel RenderModel { get; set; }

        public void DrawShape(object dc, IRenderContext renderer, ISceneState scene)
        {
            if (IsVisible == true)
            {
                renderer.DrawRetranslator(dc, RenderModel, Frame.State.ModelMatrix, scene);
            }
        }

        public bool Invalidate(IRenderContext renderer)
        {
            return false;
        }
    }
}
