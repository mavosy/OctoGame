using SUP23_G9.Commands;
using SUP23_G9.ViewModels.Base;
using System;
using System.Windows.Input;

namespace SUP23_G9.ViewModels
{
    public class StartViewModel : BaseViewModel
    {

        public StartViewModel()
        {
            StartButtonCommand = new RelayCommand(parameter => StartGame());
        }

        public ICommand StartButtonCommand { get; set; }

        public Action SwitchToGameViewEvent;

        private void RaiseSwitchToGameViewEvent()
        {
            SwitchToGameViewEvent?.Invoke();
        }

        public void StartGame()
        {
            RaiseSwitchToGameViewEvent();
        }
    }
}