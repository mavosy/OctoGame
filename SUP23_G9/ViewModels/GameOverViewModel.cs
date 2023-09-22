using SUP23_G9.Commands;
using SUP23_G9.ViewModels.Base;
using SUP23_G9.Views;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SUP23_G9.ViewModels
{
    public class GameOverViewModel : BaseViewModel
    {
        /// <summary>
        /// ICommand för "Spela Igen" knappen
        /// </summary>

        public int FinalScore { get; private set; }
        public GameOverViewModel(int finalScore)
        {
            PlayAgainCommand = new RelayCommand(parameter => PlayAgain());
            PlayAgainCommand = new RelayCommand(parameter => PlayAgain());
            FinalScore = finalScore; // Antar att du har en FinalScore egenskap i GameOverViewModel
        }
        public ICommand PlayAgainCommand { get; set; }
        public string ID { get; } = Guid.NewGuid().ToString();
        public Action SwitchToGameViewEvent { get; set; }

        public GameViewModel GameviewmodelTest = new GameViewModel();

        
        public void RaiseSwitchToGameViewEvent()
        {
            Debug.WriteLine($"Invoking from GameOverViewModel with ID: {InstanceID}");
            SwitchToGameViewEvent?.Invoke();
        }

        /// <summary>
        /// Metod för att knappen spela igen ska fungera
        /// </summary>
        public void PlayAgain()
        {
            RaiseSwitchToGameViewEvent();
            //TimerViewModel._remainingSeconds = 5;

            //// Skapa en instans av "MainWindow"
            //MainWindow mainWindow = new MainWindow();

            //// Visa MainWindow
            //mainWindow.Show();

            //// Stäng det nuvarande fönstret (GameOverView)
            //foreach (Window window in Application.Current.Windows)
            //{
            //    if (window is GameOverView)
            //    {
            //        window.Close();
            //        break; // Stäng bara det första förekomsten av GameOverView
            //    }
            //}
        }


    }
}