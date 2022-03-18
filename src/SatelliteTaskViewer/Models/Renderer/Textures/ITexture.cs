﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SatelliteTaskViewer;


namespace SatelliteTaskViewer.Models.Renderer
{
    public interface ITexture : IDisposable
    {
        ITextureDescription Description { get; }

        object Handle { get; }

        TextureTarget Target { get; }

        void Bind();

        void UnBind(TextureTarget textureTarget);

        void GenerateMipmaps();


        void ApplySampler(ITextureSampler sampler);


        void VerifyRowAlignment(int rowAlignment);

    }


}
