using SUP23_G9.ViewModels;
using System.Diagnostics;
using System.Windows;

namespace SUP23_G9.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel(/*Width, Height*/);
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