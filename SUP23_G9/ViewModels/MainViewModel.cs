using SUP23_G9.ViewModels.Base;
using System.Diagnostics;

namespace SUP23_G9.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            SwitchToStartView();
        }

        public BaseViewModel? CurrentViewModel { get; set; }

        public void SwitchToStartView()
        {
            CurrentViewModel = new StartViewModel();
            (CurrentViewModel as StartViewModel).SwitchToGameViewEvent = SwitchToGameView;
        }

        /// <summary>
        /// Byter användargränssnittsfönstret till GameView, genom bindings mellan Main ViewModel och MainWindow
        /// </summary>
        public void SwitchToGameView()
        {
            GameViewModel gameViewModel = new GameViewModel();
            CurrentViewModel = gameViewModel;

            gameViewModel.StartTimers();
            gameViewModel.SwitchToGameOverViewEvent = SwitchToGameOverView;
        }
        /// <summary>
        /// Byter användargränssnittsfönstret till GameOverView, genom bindings mellan Main ViewModel och MainWindow
        /// </summary>
        public void SwitchToGameOverView(int finalScore)// För Score
        {
            (CurrentViewModel as GameViewModel).StopTimers();
            CurrentViewModel = new GameOverViewModel(finalScore);
            Debug.WriteLine($"Setting up GameOverViewModel with ID: {CurrentViewModel.InstanceID}");
            (CurrentViewModel as GameOverViewModel).SwitchToGameViewEvent = SwitchToGameView;

        }

        //TODO Vad gör denna, behövs den? Verkar inte användas i dagsläget
        private void OnGameOver(int finalScore)// För Score
        {
            SwitchToGameOverView(finalScore);
        }
    }
}