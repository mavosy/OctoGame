using SUP23_G9.Commands;
using SUP23_G9.ViewModels.Base;
using System;
using System.Diagnostics;
using System.Windows.Input;

namespace SUP23_G9.ViewModels
{
    public class GameOverViewModel : BaseViewModel
    {
        public GameOverViewModel(int finalScore)
        {
            PlayAgainCommand = new RelayCommand(parameter => PlayAgain());
            FinalScore = finalScore; // Antar att du har en FinalScore egenskap i GameOverViewModel
        }
        public int FinalScore { get; private set; }
        public ICommand PlayAgainCommand { get; set; }
        public Action SwitchToGameViewEvent { get; set; }

        public void RaiseSwitchToGameViewEvent()
        {
            Debug.WriteLine($"Invoking from GameOverViewModel with ID: {InstanceID}");
            SwitchToGameViewEvent?.Invoke();
        }

        public void PlayAgain()
        {
            RaiseSwitchToGameViewEvent();
        }
    }
}