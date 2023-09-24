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
        public GameOverViewModel(int finalScore)
        {
            PlayAgainCommand = new RelayCommand(parameter => PlayAgain());
            FinalScore = finalScore; // Antar att du har en FinalScore egenskap i GameOverViewModel
            Debug.WriteLine($"Initializing new instance of GameOverViewModel with ID: {InstanceID}");
        }
        public int FinalScore { get; private set; }
        /// <summary>
        /// ICommand för "Spela Igen" knappen
        /// </summary>
        public ICommand PlayAgainCommand { get; set; }
        public Action SwitchToGameViewEvent { get; set; }

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
        }
    }
}