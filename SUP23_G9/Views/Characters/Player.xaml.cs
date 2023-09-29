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

        /// <summary>
        /// Eventhandler för att fånga upp när UserControl Player laddas in i gränssnittet. Sätter datacontext till objektet PlayerVM av typen PlayerViewModel som skapas i GameViewModels konstruktor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is GameViewModel gameViewModel)
            {
                this.DataContext = gameViewModel.PlayerVM;
            }

            playerControl.Focus();
        }
        /// <summary>
        /// Hanterar när en knapp trycks ner, skickar vidare argument till PlayerViewModel där de hanteras vidare
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void playerControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (ViewModel == null) return;
            ViewModel.HandleKeyDown(e);
        }
        /// <summary>
        /// Hanterar när en knapp släpps skickar vidare argument till PlayerViewModel där de hanteras vidare
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void playerControl_KeyUp(object sender, KeyEventArgs e)
        {
            if (ViewModel == null) return;
            ViewModel.HandleKeyUp(e);
        }
    }
}
