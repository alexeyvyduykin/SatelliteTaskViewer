using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using SatelliteTaskViewer.Models;
using SatelliteTaskViewer.Models.Editor;
using SatelliteTaskViewer.Models.Renderer;
using SatelliteTaskViewer.ViewModels.Containers;
using System;
using System.Reactive;

namespace SatelliteTaskViewer.ViewModels.Editor
{
    public class MainViewModel : ViewModelBase
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

        public MainViewModel(IServiceProvider serviceProvider)
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

            FromJson = ReactiveCommand.Create(FromJsonImpl);
            FromDatabase = ReactiveCommand.Create(FromDatabaseImpl);
        }

        public ReactiveCommand<Unit, Unit> FromJson { get; }

        public ReactiveCommand<Unit, Unit> FromDatabase { get; }

        [Reactive]
        public Scenario Scenario { get; private set; }

        public IEditorTool CurrentTool => _currentTool.Value;

        public IRenderContext Renderer => _renderer.Value;

        public IPresenterContract Presenter => _presenter.Value;

        public IContainerFactory ContainerFactory => _containerFactory.Value;

        public IFactory Factory => _factory.Value;

        public IEditorCanvasPlatform CanvasPlatform => _canvasPlatform.Value;

        public IJsonSerializer JsonSerializer => _jsonSerializer.Value;

        public IFileSystem FileIO => _fileIO.Value;

        public IProjectEditorPlatform Platform => _platform.Value;

        private async void FromJsonImpl()
        {
            var scenario = await ContainerFactory.GetFromJson();

            OnOpenScenario(scenario);
        }

        private async void FromDatabaseImpl()
        {
            var scenario = await ContainerFactory.GetFromDatabase();

            OnOpenScenario(scenario);
        }

        private void OnOpenScenario(Scenario? scenario)
        {
            try
            {
                if (scenario != null)
                {
                    Scenario = scenario;

                    Scenario.LogicalUpdate();

                    CanvasPlatform?.InvalidateControl?.Invoke();
                }
            }
            catch //(Exception ex)
            {
                // Log?.LogException(ex);
            }
        }
    }
}
