using SUP23_G9.ViewModels.Base;
using SUP23_G9.Views.Characters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
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
using static System.Net.Mime.MediaTypeNames;
using Application = System.Windows.Application;

namespace SUP23_G9.Views
{
    /// <summary>
    /// Interaction logic for GameView.xaml
    /// </summary>
    public partial class GameView : UserControl, INotifyPropertyChanged
    {

        Timer? _timer = new();     //nyar upp en ny timer från namespace System.Timers
        bool _leftButtonIsDown, _rightButtonIsDown, _upButtonIsDown, _downButtonIsDown;   //variabel för om en given knapp är nertryckt eller släppt
        readonly int _playerSpeed = 5;    //sätter spelarens hastighet
        //readonly int mobSpeed = 5;

        public GameView()
        {
            InitializeComponent();
            player.Focus();
            //PlayerCharacter character = new PlayerCharacter();
            _timer.Interval = 10;
            _timer.Elapsed += MovePlayerEvent;
            _timer.Start();
        }
        /// <summary>
        /// Fångar KeyDown-eventet när man trycker ner tangenten
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void player_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.A:
                    _leftButtonIsDown = true;
                    break;
                case Key.D:
                    _rightButtonIsDown = true;
                    break;
                case Key.W:
                    _upButtonIsDown = true;
                    break;
                case Key.S:
                    _downButtonIsDown = true;
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Fångar KeyUp-eventet och stoppar spelarens rörelse när man släpper tangenten
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void player_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.A:
                    _leftButtonIsDown = false;
                    break;
                case Key.D:
                    _rightButtonIsDown = false;
                    break;
                case Key.W:
                    _upButtonIsDown = false;
                    break;
                case Key.S:
                    _downButtonIsDown = false;
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Förflyttar spelare beroende på vilken tangentbordsknapp man trycker på
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MovePlayerEvent(object? sender, EventArgs e)
        {
            if (_leftButtonIsDown && Dispatcher.Invoke(() => Canvas.GetLeft(player) > 5))
            {
                Dispatcher.Invoke(() => Canvas.SetLeft(player, Canvas.GetLeft(player) - _playerSpeed)); //sätter ny x-koordinat till den förra koordinaten minus _playerSpeed
                //todo fixa lagget
                //koden gör att Kraken vänder på sig beroende på vilket håll den rör sig åt (vänster eller höger). Gör tyvärr att det laggar lite.
                //Dispatcher.Invoke(() => player.Source = new BitmapImage(new Uri(@"/Views/Components/Images/Happy_Kraken_Left.bmp", UriKind.Relative))); 
            }
            if (_rightButtonIsDown && Dispatcher.Invoke(() => Canvas.GetLeft(player) + (player.Width) < Application.Current.MainWindow.Width - 70))
            {
                Dispatcher.Invoke(() => Canvas.SetLeft(player, Canvas.GetLeft(player) + _playerSpeed));
                //todo fixa lagget
                //koden gör att Kraken vänder på sig beroende på vilket håll den rör sig åt (vänster eller höger). Gör tyvärr att det laggar lite.
                //Dispatcher.Invoke(() => player.Source = new BitmapImage(new Uri(@"/Views/Components/Images/Happy_Kraken_Right.bmp", UriKind.Relative)));

            }
            if (_upButtonIsDown && Dispatcher.Invoke(() => Canvas.GetTop(player) > 5))
            {
                Dispatcher.Invoke(() => Canvas.SetTop(player, Canvas.GetTop(player) - _playerSpeed));
            }
            if (_downButtonIsDown && Dispatcher.Invoke(() => Canvas.GetTop(player) + (player.Height) < Application.Current.MainWindow.Height - 100))
            {
                Dispatcher.Invoke(() => Canvas.SetTop(player, Canvas.GetTop(player) + _playerSpeed));
            }
            //switch (_move)
            //{
            //    case Key.A:
            //        Dispatcher.Invoke(() => Canvas.SetLeft(cube, Canvas.GetLeft(cube) - cubeSpeed));
            //        break;
            //    case Key.D:
            //        Dispatcher.Invoke(() => Canvas.SetLeft(cube, Canvas.GetLeft(cube) + cubeSpeed));
            //        break;
            //    case Key.W:
            //        Dispatcher.Invoke(() => Canvas.SetTop(cube, Canvas.GetTop(cube) - cubeSpeed));
            //        break;
            //    case Key.S:
            //        Dispatcher.Invoke(() => Canvas.SetTop(cube, Canvas.GetTop(cube) + cubeSpeed));
            //        break;

            //    default:
            //        break;
            //}
        }



        //private void MovePlayerCharacter(Key key)
        //{
        //    int cubeSpeed = 10;
        //    if (key == Key.A)
        //    {
        //        Dispatcher.Invoke(() =>
        //        {
        //            Canvas.SetLeft(cube, Canvas.GetLeft(cube) - cubeSpeed);
        //        });
        //    }
        //    if (true)
        //    {
        //        Dispatcher.Invoke(() =>
        //        {
        //            Canvas.SetLeft(cube, Canvas.GetLeft(cube) - cubeSpeed);
        //        });
        //    }            
        //    if (true)
        //    {
        //        Dispatcher.Invoke(() =>
        //        {
        //            Canvas.SetLeft(cube, Canvas.GetLeft(cube) - cubeSpeed);
        //        });
        //    }
        //    if (true)
        //    {
        //        Dispatcher.Invoke(() =>
        //        {
        //            Canvas.SetLeft(cube, Canvas.GetLeft(cube) - cubeSpeed);
        //        });
        //    }
        //}

        //private void MovePlayerCharacter(Key key)
        //{
        //    int cubeSpeed = 3;
        //    switch (key)
        //    {
        //        case Key.A:
        //            Dispatcher.Invoke(() => Canvas.SetLeft(cube, Canvas.GetLeft(cube) - cubeSpeed));
        //            break;
        //        case Key.D:
        //            Dispatcher.Invoke(() => Canvas.SetLeft(cube, Canvas.GetLeft(cube) + cubeSpeed));
        //            break;
        //        case Key.W:
        //            Dispatcher.Invoke(() => Canvas.SetTop(cube, Canvas.GetTop(cube) - cubeSpeed));
        //            break;
        //        case Key.S:
        //            Dispatcher.Invoke(() => Canvas.SetTop(cube, Canvas.GetTop(cube) + cubeSpeed));
        //            break;

        //        default:
        //            break;
        //    }
        //}
    }
}
