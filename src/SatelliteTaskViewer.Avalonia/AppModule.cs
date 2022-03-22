using Autofac;
using Microsoft.Extensions.Configuration;
using SatelliteTaskViewer.Avalonia.DatabaseProvider.PostgreSQL;
using SatelliteTaskViewer.Avalonia.DataProvider.Json;
using SatelliteTaskViewer.Avalonia.Editor;
using SatelliteTaskViewer.Avalonia.FileSystem.DotNet;
using SatelliteTaskViewer.Avalonia.ImageLoader.SOIL;
using SatelliteTaskViewer.Avalonia.ModelLoader.Assimp;
using SatelliteTaskViewer.Avalonia.Renderer.OpenTK;
using SatelliteTaskViewer.Avalonia.Serializer.Newtonsoft;
using SatelliteTaskViewer.Avalonia.ServiceProvider.Autofac;
using SatelliteTaskViewer.Avalonia.Views;
using SatelliteTaskViewer.Models;
using SatelliteTaskViewer.Models.Data;
using SatelliteTaskViewer.Models.Editor;
using SatelliteTaskViewer.Models.Renderer;
using SatelliteTaskViewer.ViewModels;
using SatelliteTaskViewer.ViewModels.Editor;
using SatelliteTaskViewer.ViewModels.Editor.Tools;
using System;
using System.IO;

namespace SatelliteTaskViewer.Avalonia
{
    public class AppModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Locator

            builder.RegisterType<AutofacServiceProvider>().As<IServiceProvider>().InstancePerLifetimeScope();


            // Build configuration

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            builder.RegisterInstance(configuration).As<IConfigurationRoot>();

            // Core

            builder.RegisterType<MainViewModel>().As<MainViewModel>().InstancePerLifetimeScope();
            builder.RegisterType<Factory>().As<IFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Configurator>().As<IConfigurator>().InstancePerLifetimeScope();
            builder.RegisterType<RenderModelFactory>().As<IRenderModelFactory>().InstancePerLifetimeScope();

            //builder.RegisterType<OpenTKShaderProgram>().As<IShaderProgram>().InstancePerDependency();

            // Dependencies

            //builder.RegisterType<AvaloniaTextClipboard>().As<ITextClipboard>().InstancePerLifetimeScope();  
            builder.RegisterType<DotNetFileSystem>().As<IFileSystem>().InstancePerLifetimeScope();
            //builder.RegisterType<RoslynScriptRunner>().As<IScriptRunner>().InstancePerLifetimeScope();
            builder.RegisterType<NewtonsoftJsonSerializer>().As<IJsonSerializer>().InstancePerLifetimeScope();
            //builder.RegisterType<DataUpdater>().As<IDataUpdater>().InstancePerLifetimeScope();

            // Data

            builder.RegisterType<PostgreSQLDatabaseProvider>().As<IDatabaseProvider>().InstancePerDependency();// InstancePerLifetimeScope();
            builder.RegisterType<JsonDataProvider>().As<IJsonDataProvider>().InstancePerLifetimeScope();

            // Renderer

            builder.RegisterType<OpenTKRenderer>().As<IRenderContext>().InstancePerLifetimeScope();
            builder.RegisterType<OpenTKPresenter>().As<IPresenterContract>().InstancePerLifetimeScope();

            //   builder.RegisterType</*OpenTK*/Device>().As<IDevice>().InstancePerLifetimeScope();
            //   builder.RegisterType<SOILDDSLoader>().As<IDDSLoader>().InstancePerLifetimeScope();
            builder.RegisterType<AssimpModelLoader>().As<IModelLoader>().InstancePerLifetimeScope();

            builder.RegisterType<ImageLibrary>().As<IImageLibrary>().InstancePerLifetimeScope();
            //  builder.RegisterType<PfimImageLoader>().As<IImageLoader>().InstancePerLifetimeScope();
            builder.RegisterType<SOILImageLoader>().As<IImageLoader>().InstancePerLifetimeScope();

            // Editor

            //  builder.RegisterType<AvaloniaImageImporter>().As<IImageImporter>().InstancePerLifetimeScope();
            builder.RegisterType<AvaloniaProjectEditorPlatform>().As<IProjectEditorPlatform>().InstancePerLifetimeScope();
            builder.RegisterType<AvaloniaEditorCanvasPlatform>().As<IEditorCanvasPlatform>().InstancePerLifetimeScope();
            builder.RegisterType<ToolDefault>().As<IEditorTool>().InstancePerLifetimeScope();

            //builder.RegisterType<AdvancedSceneTimer>().As<ISceneTimer>().InstancePerDependency();// InstancePerLifetimeScope();

            // View

            builder.RegisterType<MainWindow>().As<MainWindow>().InstancePerLifetimeScope();
        }
    }
}
