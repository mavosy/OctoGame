using SUP23_G9.ViewModels.Base;
using System;
using System.Diagnostics;
using System.Windows.Media.Media3D;

namespace SUP23_G9.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public const double defaultWindowWidth = 1000;
        public const double defaultWindowHeight = 600;
        public const double windowWidthCorrection = 17;
        public const double windowHeightCorrection = 40;

        public MainViewModel(/*double defaultWidth, double defaultHeight*/)
        {
            
            //_defaultWidth = Math.Round(defaultWidth - windowWidthCorrection);
            //_defaultHeight = Math.Round(defaultHeight - windowHeightCorrection);
            SetToStartView();
        }

        public BaseViewModel? CurrentViewModel { get; set; }
        public double WindowWidth { get; set; }
        public double WindowHeight { get; set; }
        public Action<double, double> WindowSizeChangedHandler { get; set; }


        /// <summary>
        /// Byter UI-fönstret till StartView, genom bindings för CurrentViewModel mellan MainViewModel och MainWindow
        /// </summary>
        public void SetToStartView()
        {
            CurrentViewModel = new StartViewModel();
            (CurrentViewModel as StartViewModel).SwitchToGameViewEvent = SwitchToGameView;
        }

        /// <summary>
        /// Byter UI-fönstret till GameView, genom bindings för CurrentViewModel mellan MainViewModel och MainWindow
        /// </summary>
        public void SwitchToGameView()
        {
            GameViewModel gameViewModel = new GameViewModel();
            CurrentViewModel = gameViewModel;

            SetDefaultWindowSize(gameViewModel);

            WindowSizeChangedHandler = (width, height) =>
            {
                double correctedWindowWidth = width - windowWidthCorrection;
                double correctedWindowheight = height - windowHeightCorrection;

                gameViewModel.WindowWidth = correctedWindowWidth;
                gameViewModel.WindowHeight = correctedWindowheight;
                gameViewModel.PlayerVM.WindowWidth = correctedWindowWidth;
                gameViewModel.PlayerVM.WindowHeight = correctedWindowheight;
                //Debug.WriteLine($"New value for gameViewModel.WindowWidth: {gameViewModel.WindowWidth}");
                //Debug.WriteLine($"New value for  gameViewModel.WindowHeight: {gameViewModel.WindowHeight}");
                //Debug.WriteLine($"New value for gameViewModel.PlayerVM.WindowWidth: {gameViewModel.PlayerVM.WindowWidth}");
                //Debug.WriteLine($"New value for gameViewModel.PlayerVM.WindowHeight: {gameViewModel.PlayerVM.WindowHeight}");
            };


            //Debug.WriteLine($"Setting up GameViewModel with ID: {CurrentViewModel.InstanceID}");
            gameViewModel.StartTimers();
            gameViewModel.SetToGameOverViewHandler = SwitchToGameOverView;
        }

        private void SetDefaultWindowSize(GameViewModel gameViewModel)
        {
            gameViewModel.WindowWidth = defaultWindowWidth;
            gameViewModel.WindowHeight = defaultWindowHeight;
            gameViewModel.PlayerVM.WindowWidth = defaultWindowWidth;
            gameViewModel.PlayerVM.WindowHeight = defaultWindowHeight;
        }

        /// <summary>
        /// Byter UI-fönstret till GameOverView, genom bindings för CurrentViewModel mellan MainViewModel och MainWindow
        /// </summary>
        public void SwitchToGameOverView(int finalScore)// För Score
        {
            (CurrentViewModel as GameViewModel).StopTimers();
            CurrentViewModel = new GameOverViewModel(finalScore);

            //Debug.WriteLine($"Setting up GameOverViewModel with ID: {CurrentViewModel.InstanceID}");
            (CurrentViewModel as GameOverViewModel).SetToGameViewHandler = SwitchToGameView;
        }
    }
}