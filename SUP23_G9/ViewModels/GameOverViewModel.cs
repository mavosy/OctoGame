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

        /// <summary>
        /// Slutgiltig poäng för en spelomgång
        /// </summary>
        public int FinalScore { get; private set; }
        /// <summary>
        /// Command som triggas när spelaren trycker på Spela Igen
        /// </summary>
        public ICommand PlayAgainCommand { get; set; }

        /// <summary>
        /// Action som körs i MainViewModel när spelet startas, för att byta skärmvy till spelplanen.
        /// </summary>
        public Action SetToGameViewHandler;
        /// <summary>
        /// Åberopar SetToGameViewHandler när spelaren trycker på Spela Igen
        /// </summary>
        public void RaiseSetToGameViewHandler()
        {
            SetToGameViewHandler?.Invoke();
        }
        /// <summary>
        /// Körs när PlayAgainCommand körs, när spelaren trycker på spela igen.
        /// </summary>
        public void PlayAgain()
        {
            RaiseSetToGameViewHandler();
        }
    }
}