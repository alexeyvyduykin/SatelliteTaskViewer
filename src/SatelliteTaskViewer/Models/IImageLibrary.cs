﻿using System;
using System.Collections.Generic;
using System.Text;
using SatelliteTaskViewer.Models.Image;
using SatelliteTaskViewer.Models.Renderer;

namespace SatelliteTaskViewer.Models
{
    public interface IImageLibrary
    {
        void AddKey(string key, string path);
        void AddKeys(IEnumerable<(string key, string path)> pairs);        
        bool ContainsKey(string key);

        public enum ImageLibraryState
        {
            None,    
            Loading,
            Ready
        }

        void Pass(IThreadLoadingNode node, ICache<string, int> textureCache);

        ImageLibraryState State { get; }

      //  void ThreadStart(string key);
     //   void ThreadComplete();
     //   bool IsImageReady { get; }    
     //   bool IsThreadWork { get; }
      //  IDdsImage ReadyImage { get; }
     //   string TargetKey { get; }
    }
}
