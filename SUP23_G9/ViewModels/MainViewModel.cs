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
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            SwitchToGameView();
        }
        public string ID { get; } = Guid.NewGuid().ToString();
        public BaseViewModel? CurrentViewModel { get; set; }

        /// <summary>
        /// Byter användargränssnittsfönstret till GameView, genom bindings mellan Main ViewModel och MainWindow
        /// </summary>
        public void SwitchToGameView()
        {
            CurrentViewModel = new GameViewModel();
            (CurrentViewModel as GameViewModel).SwitchToGameOverViewEvent = SwitchToGameOverView;
        }
        /// <summary>
        /// Byter användargränssnittsfönstret till GameOverView, genom bindings mellan Main ViewModel och MainWindow
        /// </summary>
        public void SwitchToGameOverView()
        {
            CurrentViewModel = new GameOverViewModel();

            Debug.WriteLine($"Setting up GameOverViewModel with ID: {CurrentViewModel.InstanceID}");
            (CurrentViewModel as GameOverViewModel).SwitchToGameViewEvent = SwitchToGameView;
        }
    }
}