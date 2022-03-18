using System;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using SatelliteTaskViewer.Models;
using SatelliteTaskViewer.Models.Editor;
using SatelliteTaskViewer.Models.Renderer;
using SatelliteTaskViewer.ViewModels.Containers;
using SatelliteTaskViewer.ViewModels.Data;
using SatelliteTaskViewer.ViewModels.Entities;
using ReactiveUI.Fody.Helpers;

namespace SatelliteTaskViewer.ViewModels.Editor
{
    public class ProjectEditorViewModel : ViewModelBase
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Lazy<IFactory> _factory;
        private readonly Lazy<IContainerFactory> _containerFactory;
        private readonly Lazy<IJsonSerializer> _jsonSerializer;
        private readonly Lazy<IFileSystem> _fileIO;
        private readonly Lazy<IRenderContext> _renderer;
        private readonly Lazy<IPresenterContract> _presenter;
        private readonly Lazy<IEditorTool> _currentTool;
        private readonly Lazy<IEditorCanvasPlatform> _canvasPlatform;
        private readonly Lazy<IProjectEditorPlatform> _platform;

        public ProjectEditorViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            _factory = _serviceProvider.GetServiceLazily<IFactory>();
            _containerFactory = _serviceProvider.GetServiceLazily<IContainerFactory>();
            _renderer = _serviceProvider.GetServiceLazily<IRenderContext>();
            _presenter = _serviceProvider.GetServiceLazily<IPresenterContract>();
            _currentTool = _serviceProvider.GetServiceLazily<IEditorTool>();
            _jsonSerializer = _serviceProvider.GetServiceLazily<IJsonSerializer>();
            _fileIO = _serviceProvider.GetServiceLazily<IFileSystem>();
            _platform = _serviceProvider.GetServiceLazily<IProjectEditorPlatform>();
            _canvasPlatform = _serviceProvider.GetServiceLazily<IEditorCanvasPlatform>();
        }

        [Reactive]
        public ProjectContainerViewModel Project { get; set; }
        
        [Reactive]
        public string ProjectPath { get; set; }

        public IEditorTool CurrentTool => _currentTool.Value;

        public IRenderContext Renderer => _renderer.Value;

        public IPresenterContract Presenter => _presenter.Value;

        public IContainerFactory ContainerFactory => _containerFactory.Value;

        public IFactory Factory => _factory.Value;

        public IEditorCanvasPlatform CanvasPlatform => _canvasPlatform.Value;

        public IJsonSerializer JsonSerializer => _jsonSerializer.Value;

        public IFileSystem FileIO => _fileIO.Value;

        public IProjectEditorPlatform Platform => _platform.Value;

        public void OnUpdate()
        {
            if (Project != null)
            {
                Project.CurrentScenario.LogicalUpdate();
            }
        }

        public async void OnFromDatabaseProject()
        {
            var project = await ContainerFactory.GetFromDatabase();

            OnOpenProject(project, "");
        }

        public async void OnFromJsonProject()
        {
            var project = await ContainerFactory.GetFromJson();

            OnOpenProject(project, "");
        }

        public async void OnFromDatabaseToJson()
        {
            await ContainerFactory.SaveFromDatabaseToJson();
        }

        public void OnOpenProject(ProjectContainerViewModel project, string path)
        {
            try
            {
                if (project != null)
                {
                    OnUnload();
                    OnLoad(project, path);
                    //    OnAddRecent(path, project.Name);
                    //    CanvasPlatform?.ResetZoom?.Invoke();
                    OnUpdate();
                    CanvasPlatform?.InvalidateControl?.Invoke();
                }
            }
            catch //(Exception ex)
            {
                // Log?.LogException(ex);
            }
        }

        public void OnOpenProject(string path)
        {
            try
            {
                if (FileIO != null && JsonSerializer != null)
                {
                    if (string.IsNullOrEmpty(path) == false && FileIO.Exists(path) == true)
                    {
                        var project = Factory.OpenProjectContainer(path, FileIO, JsonSerializer);
                        if (project != null)
                        {
                            OnOpenProject(project, path);
                        }
                    }
                }
            }
            catch //(Exception ex)
            {
                //Log?.LogException(ex);
            }
        }

        public void OnSaveProject(string path)
        {
            try
            {
                if (Project != null && FileIO != null && JsonSerializer != null)
                {
                    Factory.SaveProjectContainer(Project, path, FileIO, JsonSerializer);

                    if (string.IsNullOrEmpty(ProjectPath))
                    {
                        ProjectPath = path;
                    }
                }
            }
            catch //(Exception ex)
            {
                //Log?.LogException(ex);
            }
        }

        public void OnImportJson(string path)
        {
            try
            {
                var json = FileIO?.ReadUtf8Text(path);
                if (!string.IsNullOrWhiteSpace(json))
                {
                    var item = JsonSerializer.Deserialize<object>(json);
                    if (item != null)
                    {
                        OnImportObject(item, true);
                    }
                }
            }
            catch //(Exception ex)
            {
                //Log?.LogException(ex);
            }
        }

        public void OnExportJson(string path, object item)
        {
            try
            {
                var json = JsonSerializer?.Serialize(item);
                if (!string.IsNullOrWhiteSpace(json))
                {
                    FileIO?.WriteUtf8Text(path, json);
                }
            }
            catch //(Exception ex)
            {
                //Log?.LogException(ex);
            }
        }

        public void OnImportObject(object item, bool restore)
        {
            if (item is BaseEntity entity)
            {
                Project.AddEntity(entity);
            }
            else if (item is ProjectContainerViewModel project)
            {
                OnUnload();
                OnLoad(project, string.Empty);
            }
            else
            {
                throw new NotSupportedException("Not supported import object.");
            }
        }

        public void OnSetCameraTo()
        {
            if (Project?.CurrentScenario != null && Project?.Selected != null)
            {
                if (Project.Selected is ITargetable target)
                {
                    Project.CurrentScenario.SetCameraTo(target);
                }
            }
        }

        public void OnLoad(ProjectContainerViewModel project, string path = null)
        {
            if (project != null)
            {
                Project = project;
                ProjectPath = path;
            }
        }

        public void OnUnload()
        {
            if (Project is not null)
            {
                Project = null;
                ProjectPath = string.Empty;           
                GC.Collect();
            }
        }

        public string GetName(object item)
        {
            if (item != null)
            {
                if (item is ViewModelBase observable)
                {
                    return observable.Name;
                }
            }
            return string.Empty;
        }

        public void OnCloseProject()
        {
            //Project?.History?.Reset();
            OnUnload();
        }
    }
}
