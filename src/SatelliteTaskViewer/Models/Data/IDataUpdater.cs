using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SatelliteTaskViewer.ViewModels;
using SatelliteTaskViewer.ViewModels.Entities;

namespace SatelliteTaskViewer.Models.Data
{
    public interface IDataUpdater
    {
        void Update(double t, FrameViewModel frame);
    }
}
