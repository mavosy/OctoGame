using SUP23_G9.ViewModels;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace SUP23_G9.Views
{
    public partial class MainWindow : Window
    {
        private GameOverView gameOverView; // Skapa en privat medlemsvariabel för GameOverView
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}