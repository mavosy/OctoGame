using SUP23_G9.Commands;
using SUP23_G9.ViewModels.Base;
using SUP23_G9.Views;
using System.ComponentModel;
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