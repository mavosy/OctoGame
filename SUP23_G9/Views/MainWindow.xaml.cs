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
        private bool isGameOverShown = false; // Boolesk variabel för att kontrollera om GameOverView har visats

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
            //DataContext = new TimerViewModel(30);  // 1 minuts nedräkning

            // Skapa en instans av GameOverView
            gameOverView = new GameOverView();

            // Lägg till en händelsehanterare för MainWindow's Closing-händelse
            Closing += MainWindow_Closing;
        }

        // Händelsehanterare för MainWindow's Closing-händelse
        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            // Kontrollera om tiden har gått ut och om GameOverView redan har visats
            if (!isGameOverShown)
            {
                // Visa GameOverView och markera att det har visats
                gameOverView.Show();
                isGameOverShown = true;
            }
        }
    }
}
