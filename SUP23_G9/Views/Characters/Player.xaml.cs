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
        public Player()
        {
            InitializeComponent();
            //Loaded += (sender, e) => SetDataContextForPlayer();
        }

        //private void SetDataContextForPlayer()
        //{
        //    var gameViewModel = this.DataContext as GameViewModel;

        //    if (gameViewModel != null)
        //    {
        //        gameViewModel.PlayerVM = this.DataContext as PlayerViewModel;
        //    }
        //}

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
            //FindDataContext();
        }

        //private void FindDataContext()
        //{
        //    var playerViewModel = this.DataContext as PlayerViewModel;
        //    if (playerViewModel != null)
        //    {
        //        var parentContext = this.FindAncestorDataContext<GameViewModel>();
        //        if (parentContext != null)
        //        {
        //            parentContext.PlayerVM = playerViewModel;
        //        }
        //    }
        //}

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

        //public T FindAncestorDataContext<T>() where T : class
        //{
        //    DependencyObject parent = this;
        //    while (parent != null)
        //    {
        //        if (parent is FrameworkElement fe && fe.DataContext is T desiredDataContext)
        //        {
        //            return desiredDataContext;
        //        }
        //        parent = VisualTreeHelper.GetParent(parent);
        //    }
        //    return null;
        //}
    }

}
