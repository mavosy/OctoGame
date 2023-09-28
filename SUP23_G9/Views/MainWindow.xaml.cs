using SUP23_G9.ViewModels;
using System.Windows;

namespace SUP23_G9.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
            AttachSetWindowStateHandlers();
        }

        public void AttachSetWindowStateHandlers()
        {
            if (DataContext is MainViewModel mainViewModel)
            {
                mainViewModel.OnSwitchToGameView = MaximizeWindowState;
                mainViewModel.OnSwitchToGameOverView = NormalizeWindowState;
            }
        }
        public void MaximizeWindowState()
        {
            this.WindowState = WindowState.Maximized;
        }        

        public void NormalizeWindowState()
        {
            this.WindowState = WindowState.Normal;
        }

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (DataContext is MainViewModel mainViewModel)
            {
                mainViewModel.WindowSizeChangedHandler?.Invoke(e.NewSize.Width, e.NewSize.Height);
            }
        }
    }
}