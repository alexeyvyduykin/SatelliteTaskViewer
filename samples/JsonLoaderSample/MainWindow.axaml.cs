using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace JsonLoaderSample
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            //  Class2 cl2 = new Class2();
            //  cl2.Main();
        }
    }
}
