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
            SwitchToStartView();
            Debug.WriteLine($"Initializing MainViewModel with ID: {InstanceID}");
        }

        public BaseViewModel? CurrentViewModel { get; set; }

        public void SwitchToStartView()
        {
            CurrentViewModel = new StartViewModel();
            Debug.WriteLine($"Initializing StartViewModel in MainViewModel with ID: {CurrentViewModel.InstanceID}");
            (CurrentViewModel as StartViewModel).SwitchToGameViewEvent = SwitchToGameView;
        }

        /// <summary>
        /// Byter användargränssnittsfönstret till GameView, genom bindings mellan Main ViewModel och MainWindow
        /// </summary>
        public void SwitchToGameView()// Behövdes för Score
        {
            CurrentViewModel = new GameViewModel();
            Debug.WriteLine($"Initializing GameViewModel in MainViewModel with ID: {CurrentViewModel.InstanceID}");
            (CurrentViewModel as GameViewModel).SwitchToGameOverViewEvent = SwitchToGameOverView;
        }
        /// <summary>
        /// Byter användargränssnittsfönstret till GameOverView, genom bindings mellan Main ViewModel och MainWindow
        /// </summary>

        private void OnGameOver(int finalScore)// För Score
        {
            SwitchToGameOverView(finalScore);
        }
        public void SwitchToGameOverView(int finalScore)// För Score
        {
            CurrentViewModel = new GameOverViewModel(finalScore);
            Debug.WriteLine($"Initializing GameOverViewModel in MainViewModel with ID: {CurrentViewModel.InstanceID}");
            (CurrentViewModel as GameOverViewModel).SwitchToGameViewEvent = SwitchToGameView;
        
        }
    }
}