﻿using GlmSharp;
using SatelliteTaskViewer.Models;
using SatelliteTaskViewer.Models.Entities;
using SatelliteTaskViewer.Models.Renderer;
using SatelliteTaskViewer.Models.Scene;
using SatelliteTaskViewer.ViewModels.Data;
using SatelliteTaskViewer.ViewModels.Scene;
using ReactiveUI.Fody.Helpers;

namespace SatelliteTaskViewer.ViewModels.Entities
{
    public class Satellite : BaseEntity, IDrawable, ITargetable, IChildren
    {
        [Reactive]
        public FrameViewModel Frame { get; set; }

        [Reactive]
        public RenderModel RenderModel { get; set; }

        public dmat4 InverseAbsoluteModel
        {
            get
            {
                if (Frame.Parent.State is SatelliteAnimator)
                {
                    if (Frame.Parent.State is IFrameable frameable)
                    {
                        return frameable.ModelMatrix.Inverse;
                    }
                }
                return dmat4.Identity.Inverse;
            }
        }

        public void DrawShape(object dc, IRenderContext renderer, ISceneState scene)
        {
            if (IsVisible == true)
            {              
                renderer.DrawSatellite(dc, RenderModel, Frame.State.AbsoluteModelMatrix, scene);
            }
        }

        public bool Invalidate(IRenderContext renderer)
        {
            return false;
        }
    }
}
