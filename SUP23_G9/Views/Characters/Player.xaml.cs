using SUP23_G9.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SUP23_G9.Views.Characters
{
    /// <summary>
    /// Interaction logic for Player.xaml
    /// </summary>
    public partial class Player : UserControl
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public Player()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Property som används för att skicka key-event till PlayerViewModel
        /// </summary>
        private PlayerViewModel ViewModel { get { return DataContext as PlayerViewModel; } }

        /// <summary>
        /// Sätter fokus på spelare
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            krakenControl.Focus();
        }
        /// <summary>
        /// Event handler för KeyDown-eventet, skickar vidare till PlayerViewModel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void krakenControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (ViewModel == null) return;
            ViewModel.HandleKeyDown(e);
        }
        /// <summary>
        /// Event handler för KeyUp-eventet, skickar vidare till PlayerViewModel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void krakenControl_KeyUp(object sender, KeyEventArgs e)
        {
            if (ViewModel == null) return;
            ViewModel.HandleKeyUp(e);
        }
    }
}
