using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace TaskListEditorSample.Views
{
    public partial class TaskList : UserControl
    {
        public TaskList()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
