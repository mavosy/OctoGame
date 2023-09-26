using SUP23_G9.ViewModels.Base;
using System.Diagnostics;

namespace SUP23_G9.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            SetToStartView();
        }

        public BaseViewModel? CurrentViewModel { get; set; }

        /// <summary>
        /// Byter UI-fönstret till StartView, genom bindings för CurrentViewModel mellan MainViewModel och MainWindow
        /// </summary>
        public void SetToStartView()
        {
            StartViewModel startViewModel= new StartViewModel();
            CurrentViewModel = startViewModel;
            startViewModel.SwitchToGameViewEvent = SwitchToGameView;
        }

        /// <summary>
        /// Byter UI-fönstret till GameView, genom bindings för CurrentViewModel mellan MainViewModel och MainWindow
        /// </summary>
        public void SwitchToGameView()
        {
            GameViewModel gameViewModel = new GameViewModel();
            CurrentViewModel = gameViewModel;

            Debug.WriteLine($"Setting up GameViewModel with ID: {CurrentViewModel.InstanceID}");
            gameViewModel.StartTimers();
            gameViewModel.SetToGameOverViewHandler = SwitchToGameOverView;
        }

        /// <summary>
        /// Byter UI-fönstret till GameOverView, genom bindings för CurrentViewModel mellan MainViewModel och MainWindow
        /// </summary>
        public void SwitchToGameOverView(int finalScore)// För Score
        {
            (CurrentViewModel as GameViewModel).StopTimers();
            CurrentViewModel = new GameOverViewModel(finalScore);

            Debug.WriteLine($"Setting up GameOverViewModel with ID: {CurrentViewModel.InstanceID}");
            (CurrentViewModel as GameOverViewModel).SetToGameViewHandler = SwitchToGameView;
        }
    }
}