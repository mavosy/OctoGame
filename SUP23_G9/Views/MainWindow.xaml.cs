using SUP23_G9.ViewModels;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace SUP23_G9.Views
{
    public partial class MainWindow : Window
    {
        private GameOverView gameOverView; // Skapa en privat medlemsvariabel för GameOverView
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
            //DataContext = new TimerViewModel(30);  // 1 minuters nedräkning

            // Skapa en instans av GameOverView
            gameOverView = new GameOverView();

            // Lägg till en händelsehanterare för MainWindow's Closing-händelse
            Closing += MainWindow_Closing;
        }

        // Händelsehanterare för MainWindow's Closing-händelse
        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            // Visa GameOverView och stäng MainWindow
            gameOverView.Show();
            
        }
    }
}
