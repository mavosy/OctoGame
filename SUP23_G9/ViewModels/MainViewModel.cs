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
        public MainViewModel()
        {
            CurrentViewModel = new GameViewModel();
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