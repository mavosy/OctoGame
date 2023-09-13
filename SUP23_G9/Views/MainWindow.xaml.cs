using SUP23_G9.ViewModels;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace SUP23_G9.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
            //DataContext = new TimerViewModel(30);  // 1 minuters nedräkning
        }
    }
}
