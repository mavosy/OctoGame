using SUP23_G9.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUP23_G9.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        private GameViewModel gameViewModel = new GameViewModel();
        private GameOverViewModel gameOverViewModel = new GameOverViewModel();
        private StartViewModel startViewModel = new StartViewModel();
        public MainViewModel()
        {
            CurrentViewModel = new StartViewModel();
            gameViewModel.SwitchToGameOverViewEvent += () => SwitchToGameOverView();
            gameOverViewModel.SwitchToGameViewEvent += () => SwitchToGameView();
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