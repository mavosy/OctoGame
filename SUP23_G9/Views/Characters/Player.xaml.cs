using SUP23_G9.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SUP23_G9.Views.Characters
{
    /// <summary>
    /// Interaction logic for Player.xaml
    /// </summary>
    public partial class Player : UserControl
    {
        public Player()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Property som används för att skicka key-event till PlayerViewModel
        /// </summary>
        private PlayerViewModel ViewModel { get { return DataContext as PlayerViewModel; } }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is GameViewModel gameViewModel)
            {
                this.DataContext = gameViewModel.PlayerVM;
            }

            krakenControl.Focus();
        }

        private void krakenControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (ViewModel == null) return;
            ViewModel.HandleKeyDown(e);
        }

        private void krakenControl_KeyUp(object sender, KeyEventArgs e)
        {
            if (ViewModel == null) return;
            ViewModel.HandleKeyUp(e);
        }
    }
}
