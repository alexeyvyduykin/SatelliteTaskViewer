﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using SatelliteTaskViewer.ViewModels.Containers;
using Avalonia.Metadata;
using SatelliteTaskViewer.ViewModels.Editors;

namespace SatelliteTaskViewer.Avalonia.Converters
{
    public class WorkspaceTemplateSelector : IDataTemplate
    {
        public bool SupportsRecycling => false;
        
        [Content]
        public Dictionary<string, IDataTemplate> Templates { get; } = new Dictionary<string, IDataTemplate>();

        public IControl Build(object data)
        {       
            return Templates[((Workspace)data).ToString()].Build(data);
        }

        public bool Match(object data)
        {
            return data is Workspace;
        }
    }
}