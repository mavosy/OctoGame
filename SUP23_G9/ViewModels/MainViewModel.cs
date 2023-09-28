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
        //private BaseViewModel? _currentViewModel;

        //public BaseViewModel? CurrentViewModel
        //{
        //    get { return _currentViewModel; }
        //    set
        //    {
        //        _currentViewModel = value;
        //        OnChangedViewModel();
        //    }
        //}

        //private void OnChangedViewModel()
        //{
        //    if (_currentViewModel != null)
        //    {
        //        return;
        //    }
        //    else if (CurrentViewModel is GameViewModel gameViewModel)
        //    {
        //        OnChangedViewModelGetWindowSize?.Invoke();
        //        GetWindowSizeBeforeChanged = (WidthAtGameStart, HeightAtGameStart) =>
        //        {
        //            double correctedWidthAtGameStart = WidthAtGameStart - windowWidthCorrection;
        //            double correctedHeightAtGameStart = HeightAtGameStart - windowHeightCorrection;

        //            gameViewModel.WindowWidth = correctedWidthAtGameStart;
        //            gameViewModel.WindowHeight = correctedHeightAtGameStart;
        //            gameViewModel.PlayerVM.WindowWidth = correctedWidthAtGameStart;
        //            gameViewModel.PlayerVM.WindowHeight = correctedHeightAtGameStart;
        //        };
        //    }
        //}

        public double WindowWidth { get; set; }
        public double WindowHeight { get; set; }
        public Action<double, double> WindowSizeChangedHandler { get; set; }
        public Action<double, double> GetWindowSizeBeforeChanged { get; set; }

        public Action OnChangedViewModelGetWindowSize;

        public Action OnSwitchToGameView;
        public Action OnSwitchToGameOverView;

        //private void SetDefaultWindowSize(GameViewModel gameViewModel)
        //{
        //    //gameViewModel.WindowWidth = defaultWindowWidth;
        //    //gameViewModel.WindowHeight = defaultWindowHeight;
        //    //gameViewModel.PlayerVM.WindowWidth = defaultWindowWidth;
        //    //gameViewModel.PlayerVM.WindowHeight = defaultWindowHeight;
        //}

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

            //SetDefaultWindowSize(gameViewModel);

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
            gameViewModel.StartTimers();
            OnSwitchToGameView?.Invoke();
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