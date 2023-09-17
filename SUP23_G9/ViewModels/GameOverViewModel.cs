using SUP23_G9.Commands;
using SUP23_G9.ViewModels.Base;
using SUP23_G9.Views;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace SUP23_G9.ViewModels
{
    public class GameOverViewModel : BaseViewModel
    {
        /// <summary>
        /// ICommand för "Spela Igen" knappen
        /// </summary>
        public ICommand PlayAgainCommand { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public GameOverViewModel()
        {
            // Skapa ett nytt RelayCommand för "Spela Igen" knappen
            PlayAgainCommand = new RelayCommand(parameter => PlayAgain());
        }

        /// <summary>
        /// Metod för att knappen spela igen ska fungera
        /// </summary>
        public void PlayAgain()
        {
            // Skapa en instans av "MainWindow"
            MainWindow mainWindow = new MainWindow();

            // Visa MainWindow
            mainWindow.Show();

            // Stäng det nuvarande fönstret (GameOverView)
            foreach (Window window in Application.Current.Windows)
            {
                if (window is GameOverView)
                {
                    window.Close();
                    break; // Stäng bara det första förekomsten av GameOverView
                }
            }
        }
    }
}
