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
        /// <summary>
        /// Fäster metoder för maximering och normalisering av programfönstret vid separata Actions
        /// </summary>
        public void AttachSetWindowStateHandlers()
        {
            if (DataContext is MainViewModel mainViewModel)
            {
                mainViewModel.OnSwitchToGameView = MaximizeWindowState;
                mainViewModel.OnSwitchToGameOverView = NormalizeWindowState;
            }
        }
        /// <summary>
        /// Maximerar programfönstret
        /// </summary>
        public void MaximizeWindowState()
        {
            this.WindowState = WindowState.Maximized;
        }        
        /// <summary>
        /// Normaliserar programfönstret
        /// </summary>
        public void NormalizeWindowState()
        {
            this.WindowState = WindowState.Normal;
        }
        /// <summary>
        /// Event som reagerar på när programfönstrets storlek förändras. Skickar ny storlek som parameter i en WindowSizeChangedHandler-action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (DataContext is MainViewModel mainViewModel)
            {
                mainViewModel.WindowSizeChangedHandler?.Invoke(e.NewSize.Width, e.NewSize.Height);
            }
        }
    }
}