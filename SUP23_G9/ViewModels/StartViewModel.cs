using SUP23_G9.Commands;
using SUP23_G9.Views;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace SUP23_G9.ViewModels
{
    public class StartViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

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
            // Skapa en instans av "MainWindow"
            MainWindow mainWindow = new MainWindow();

            // Visa MainWindow
            mainWindow.Show();

            // Stäng det nuvarande fönstret (StartView)
            Application.Current.MainWindow.Close();
        }
    }
}