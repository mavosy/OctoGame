using SUP23_G9.ViewModels;
using SUP23_G9.ViewModels.Base;
using SUP23_G9.Views.Components;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SUP23_G9.Views
{
    /// <summary>
    /// Interaction logic for GameView.xaml
    /// </summary>
    public partial class GameView : UserControl
    {
        public GameView()
        {
            InitializeComponent();

            gameArea.SizeChanged += GameArea_SizeChanged;
            GlobalStatic._gameAreaRenderedWidth = gameArea.ActualWidth;
            GlobalStatic._gameAreaRenderedHeight = gameArea.ActualHeight;
        }

        private void GameArea_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            GlobalStatic._gameAreaRenderedWidth = gameArea.ActualWidth;
            GlobalStatic._gameAreaRenderedHeight = gameArea.ActualHeight;
        }
    }
}