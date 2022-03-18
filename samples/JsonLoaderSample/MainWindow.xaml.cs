using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace JsonLoaderSample
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);


          //  Class2 cl2 = new Class2();
          //  cl2.Main();
        }
    }
}
