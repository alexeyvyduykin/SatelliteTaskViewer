using Autofac;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using SatelliteTaskViewer.Avalonia.Views;
using SatelliteTaskViewer.Models.Editor;
using SatelliteTaskViewer.ViewModels.Editor;
using System;
using System.Text;

namespace SatelliteTaskViewer.Avalonia
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        //public static void InitializeDesigner()
        //{
        //    if (Design.IsDesignMode)
        //    {
        //        var builder = new ContainerBuilder();

        //        builder.RegisterModule<AppModule>();

        //        var container = builder.Build();

        //        DesignerContext.InitializeContext(container.Resolve<IServiceProvider>());
        //    }
        //}

        //public override void OnFrameworkInitializationCompleted()
        //{
        //    if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        //    {
        //        desktop.MainWindow = new MainWindow
        //        {
        //            DataContext = new MainWindowViewModel(),
        //        };
        //    }

        //    base.OnFrameworkInitializationCompleted();
        //}
        public override void OnFrameworkInitializationCompleted()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktopLifetime)
            {
                InitializationClassicDesktopStyle(desktopLifetime);
            }

            base.OnFrameworkInitializationCompleted();
        }

        private void InitializationClassicDesktopStyle(IClassicDesktopStyleApplicationLifetime desktopLifetime)
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<AppModule>();

            var container = builder.Build();

            var serviceProvider = container.Resolve<IServiceProvider>();
            var containerFactory = serviceProvider.GetService<IContainerFactory>();
            var editor = serviceProvider.GetService<ProjectEditorViewModel>();


            //  editor.OnOpenProject(containerFactory.GetDemo(), "");

            //  editor.OnOpenProject(containerFactory.GetEmptyProject(), "");


            //  editor.IsToolIdle = true;

            var mainWindow = serviceProvider.GetService<MainWindow>();
            // var mainControl = mainWindow.FindControl<MainControl>("MainControl");

            mainWindow.DataContext = editor;

            mainWindow.Closing += (sender, e) => { };

            desktopLifetime.MainWindow = mainWindow;

            desktopLifetime.Exit += (sennder, e) =>
            {
                container.Dispose();
            };
        }

    }
}
