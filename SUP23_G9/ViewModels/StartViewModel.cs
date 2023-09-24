using SUP23_G9.Commands;
using SUP23_G9.ViewModels.Base;
using SUP23_G9.Views;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace SUP23_G9.ViewModels
{
    public class StartViewModel : BaseViewModel
    {
        public ICommand StartCommand { get; set; }

        public StartViewModel()
        {
            StartCommand = new RelayCommand(parameter => StartGame());
            Debug.WriteLine($"Initializing new instance of StartViewModel with ID: {InstanceID}");
        }

        public Action SwitchToGameViewEvent;

        private void RaiseSwitchToGameViewEvent()
        {
            SwitchToGameViewEvent?.Invoke();
        }
        public void StartGame()
        {
            RaiseSwitchToGameViewEvent();
            //// Skapa en instans av "MainWindow"
            //MainWindow mainWindow = new MainWindow();

            //// Visa MainWindow
            //mainWindow.Show();

            //// Stäng det nuvarande fönstret (StartView)
            //Application.Current.MainWindow.Close();
        }
    }
}