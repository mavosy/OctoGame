using SUP23_G9.ViewModels.Base;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Media.Media3D;

namespace SUP23_G9.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        //public const double defaultWindowWidth = 1400;
        //public const double defaultWindowHeight = 800;
        public const double windowWidthCorrection = 17;
        public const double windowHeightCorrection = 40;

        public MainViewModel()
        {
            SetToStartView();
        }

        public BaseViewModel? CurrentViewModel { get; set; }

        public double WindowWidth { get; set; }
        public double WindowHeight { get; set; }
        public Action<double, double> WindowSizeChangedHandler { get; set; }
        public Action<double, double> GetWindowSizeBeforeChanged { get; set; }

        public Action OnChangedViewModelGetWindowSize;

        public Action OnSwitchToGameView;
        public Action OnSwitchToGameOverView;

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

            WindowSizeChangedHandler = (width, height) =>
            {
                double correctedWindowWidth = width - windowWidthCorrection;
                double correctedWindowheight = height - windowHeightCorrection;

                gameViewModel.WindowWidth = correctedWindowWidth;
                gameViewModel.WindowHeight = correctedWindowheight;
                gameViewModel.PlayerVM.WindowWidth = correctedWindowWidth;
                gameViewModel.PlayerVM.WindowHeight = correctedWindowheight;
            };


            //Debug.WriteLine($"Setting up GameViewModel with ID: {CurrentViewModel.InstanceID}");
            OnSwitchToGameView?.Invoke();
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

            //Debug.WriteLine($"Setting up GameOverViewModel with ID: {CurrentViewModel.InstanceID}");
            OnSwitchToGameOverView?.Invoke();
            (CurrentViewModel as GameOverViewModel).SetToGameViewHandler = SwitchToGameView;
        }
    }
}