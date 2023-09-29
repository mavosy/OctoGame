using SUP23_G9.Commands;
using SUP23_G9.ViewModels.Base;
using System;
using System.Windows.Input;

namespace SUP23_G9.ViewModels
{
    public class StartViewModel : BaseViewModel
    {
        /// <summary>
        /// Command som triggas när spelaren trycker på start-knappen 
        /// </summary>
        public ICommand StartButtonCommand { get; set; }

        public StartViewModel()
        {
            StartButtonCommand = new RelayCommand(parameter => StartGame());
        }
        /// <summary>
        /// Action som körs i MainViewModel när spelet börjar, för att byta skärmvy till spelplanen.
        /// </summary>
        public Action SetToGameViewEvent;
        /// <summary>
        /// Åberopar SetToGameViewHandler när spelaren trycker på start-knappen
        /// </summary>
        private void RaiseSwitchToGameViewEvent()
        {
            SetToGameViewEvent?.Invoke();
        }
        /// <summary>
        /// Körs när StartCommand körs, när spelaren trycker på start-knappen
        /// </summary>
        public void StartGame()
        {
            RaiseSwitchToGameViewEvent();
        }
    }
}