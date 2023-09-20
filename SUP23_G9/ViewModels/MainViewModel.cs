using SUP23_G9.ViewModels.Base;
using SUP23_G9.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUP23_G9.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        private GameViewModel gameViewModel = new GameViewModel();
        private GameOverViewModel gameOverViewModel = new GameOverViewModel();
        private GameOverView gameOverView = new GameOverView();
        public MainViewModel()
        {
            CurrentViewModel = new GameViewModel();
            gameOverViewModel.SwitchToGameViewEvent += () =>
            {
                SwitchToGameView();
                Debug.WriteLine("SwitchToGameView event handler executed in MainViewModel.");
            };
            gameViewModel.SwitchToGameOverViewEvent += () =>
            {
                SwitchToGameOverView();
                Debug.WriteLine("SwitchToGameOverView event handler executed in MainViewModel.");
            };
            gameOverView.SwitchToGameViewEvent += () =>
            {
                SwitchToGameView();
                Debug.WriteLine("SwitchToGameView event handler executed in MainViewModel.");
            };
        }

        public BaseViewModel? CurrentViewModel { get; set; }

        public void DisposeViewModel() => CurrentViewModel = null;
        /// <summary>
        /// Byter användargränssnittsfönstret mot GameOverView, genom bindings mellan Main ViewModel och MainWindow
        /// </summary>
        public void SwitchToGameOverView() => CurrentViewModel = new GameOverViewModel();

        /// <summary>
        /// Byter användargränssnittsfönstret mot GameView, genom bindings mellan Main ViewModel och MainWindow
        /// </summary>
        public void SwitchToGameView() => CurrentViewModel = new GameViewModel();
    }
}