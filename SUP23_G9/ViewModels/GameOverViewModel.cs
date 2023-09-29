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
            FinalScore = finalScore;
        }

        public int FinalScore { get; private set; }
        public ICommand PlayAgainCommand { get; set; }

        public Action SetToGameViewHandler;
        public void RaiseSetToGameViewHandler()
        {
            //Debug.WriteLine($"Invoking from GameOverViewModel with ID: {InstanceID}");
            SetToGameViewHandler?.Invoke();
        }

        public void PlayAgain()
        {
            RaiseSetToGameViewHandler();
        }
    }
}