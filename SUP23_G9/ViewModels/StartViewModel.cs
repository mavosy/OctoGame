using SUP23_G9.Commands;
using SUP23_G9.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace SUP23_G9.ViewModels
{
    internal class StartViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private ICommand _startCommand;

        public ICommand StartCommand
        {
            get { return _startCommand; }
            set
            {
                if (_startCommand != value)
                {
                    _startCommand = value;
                    OnPropertyChanged(nameof(StartCommand));
                }
            }
        }

        // Konstruktor
        public StartViewModel()
        {
            StartCommand = new RelayCommand(parameter => StartGame());
           
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void StartGame()
        {
            // Skapa en instans av din MainWindow
            MainWindow mainWindow = new MainWindow();

            // Visa huvudfönstret med Show
            mainWindow.Show();

            // Stäng det nuvarande fönstret
            Application.Current.MainWindow.Close();
        }
    }
}