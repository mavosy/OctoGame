using SUP23_G9.ViewModels.Base;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Media.Media3D;

namespace SUP23_G9.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public const double windowWidthCorrection = 17;
        public const double windowHeightCorrection = 40;

        public MainViewModel()
        {
            SetToStartView();
        }
        /// <summary>
        /// Håller nuvarande vymodell och binder till en ContentControl i MainWindow.xaml. Ny vymodell sätts genom metoder i MainViewModel
        /// </summary>
        public BaseViewModel? CurrentViewModel { get; set; }
        /// <summary>
        /// Håller värden för programfönstrets förändrade storlek, sätts efter att en förändring har skett.
        /// </summary>
        public Action<double, double> WindowSizeChangedHandler { get; set; }
        /// <summary>
        /// Action som triggas när UI-fönstret byts till GameView
        /// </summary>
        public Action OnSwitchToGameView;
        /// <summary>
        /// Action som triggas när UI-fönstret byts till GameOverView
        /// </summary>
        public Action OnSwitchToGameOverView;

        /// <summary>
        /// Byter UI-fönstret till StartView, genom bindings för CurrentViewModel mellan MainViewModel och MainWindow
        /// </summary>
        public void SetToStartView()
        {
            CurrentViewModel = new StartViewModel();
            (CurrentViewModel as StartViewModel).SetToGameViewEvent = SwitchToGameView;
        }

        /// <summary>
        /// Byter UI-fönstret till GameView, genom bindings för CurrentViewModel mellan MainViewModel och MainWindow. Startar timers för spelet och förbereder för att byta till GameOverViewModel
        /// </summary>
        public void SwitchToGameView()
        {
            GameViewModel gameViewModel = new GameViewModel();
            CurrentViewModel = gameViewModel;

            SetWindowSizeToNewSize(gameViewModel);


            OnSwitchToGameView?.Invoke();
            gameViewModel.StartTimers();
            gameViewModel.SetToGameOverViewHandler = SwitchToGameOverView;
        }
        /// <summary>
        /// Hämtar nya värden från WindowSizeChangedHandler, korrigerar för felaktigheter och disponerar värdena till egenskaper i GameViewModel och PlayerViewModel
        /// </summary>
        /// <param name="gameViewModel"></param>
        private void SetWindowSizeToNewSize(GameViewModel gameViewModel)
        {
            WindowSizeChangedHandler = (width, height) =>
            {
                double correctedWindowWidth = width - windowWidthCorrection;
                double correctedWindowheight = height - windowHeightCorrection;

                gameViewModel.WindowWidth = correctedWindowWidth;
                gameViewModel.WindowHeight = correctedWindowheight;
                gameViewModel.PlayerVM.WindowWidth = correctedWindowWidth;
                gameViewModel.PlayerVM.WindowHeight = correctedWindowheight;
            };
        }

        /// <summary>
        /// Byter UI-fönstret till GameOverView, genom bindings för CurrentViewModel mellan MainViewModel och MainWindow
        /// </summary>
        public void SwitchToGameOverView(int finalScore)
        {
            (CurrentViewModel as GameViewModel).StopTimers();
            CurrentViewModel = new GameOverViewModel(finalScore);

            OnSwitchToGameOverView?.Invoke();
            (CurrentViewModel as GameOverViewModel).SetToGameViewHandler = SwitchToGameView;
        }
    }
}