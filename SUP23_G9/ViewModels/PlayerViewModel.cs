using SUP23_G9.Commands;
using SUP23_G9.ViewModels.Base;
using SUP23_G9.Views.Characters;
using SUP23_G9.Views.Components;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace SUP23_G9.ViewModels
{

    public class PlayerViewModel : BaseViewModel
    {
        readonly double _distanceToEdge = 5;
        readonly double _playerSpeed = 5;

        bool _leftButtonIsDown, _rightButtonIsDown, _upButtonIsDown, _downButtonIsDown;
        DispatcherTimer _timer;

        public PlayerViewModel()
        {
            LeftCoordinates = 400;
            TopCoordinates = 400;
            Width = 50;
            Height = 50;

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(10);

            //kör MovePlayerEvent varje gång interval ska börja om
            _timer.Tick += MovePlayerEvent;
            _timer.Start();


            //sätter ett startvärde på speglingen av spelarbilden
            FlipImageX = 1.0;

            LoadKrakenImageProcessing();
            UpKeyDownCommand = new RelayCommand(x => MovePlayerUp(), x => IsNotAtTopEdge());
            DownKeyDownCommand = new RelayCommand(x => MovePlayerDown(), x => IsNotAtBottomEdge());
            LeftKeyDownCommand = new RelayCommand(x => MovePlayerLeft(), x => IsNotAtLeftEdge());
            RightKeyDownCommand = new RelayCommand(x => MovePlayerRight(), x => IsNotAtRightEdge());
        }

        public double LeftCoordinates { get; set; }
        public double TopCoordinates { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public ICommand UpKeyDownCommand { get; private set; }
        public ICommand DownKeyDownCommand { get; private set; }
        public ICommand LeftKeyDownCommand { get; private set; }
        public ICommand RightKeyDownCommand { get; private set; }
        public BitmapImage PlayerImage { get; set; }
        public double FlipImageX { get; set; }

        private void MovePlayerEvent(object? sender, EventArgs e)
        {
            GlobalVariabels._playerCoordinatesLeft = LeftCoordinates;
            GlobalVariabels._playerCoordinatesTop = TopCoordinates;
            if (_leftButtonIsDown && IsNotAtLeftEdge())
            {
                MovePlayerLeft();
                TurnSpriteBack();
            }

            if (_rightButtonIsDown && IsNotAtRightEdge())
            {
                MovePlayerRight();
                TurnSpriteHorizontally();
            }

            if (_upButtonIsDown && IsNotAtTopEdge())
            {
                MovePlayerUp();
            }

            if (_downButtonIsDown && IsNotAtBottomEdge())
            {
                MovePlayerDown();
            }
        }

        public void MovePlayerLeft() => LeftCoordinates -= _playerSpeed;

        public void MovePlayerRight() => LeftCoordinates += _playerSpeed;

        public void MovePlayerUp() => TopCoordinates -= _playerSpeed;

        public void MovePlayerDown() => TopCoordinates += _playerSpeed;

        public bool IsNotAtLeftEdge() => LeftCoordinates > _distanceToEdge;

        public bool IsNotAtRightEdge() => (LeftCoordinates + Width) < (985 - _distanceToEdge); //Det som styr hur långt ut till kanten han kan "simma"

        public bool IsNotAtTopEdge() => TopCoordinates > _distanceToEdge;

        public bool IsNotAtBottomEdge() => (TopCoordinates + Height) < (565 - _distanceToEdge); //Det som styr hur långt ner till kanten han kan "simma"

        internal void HandleKeyDown(KeyEventArgs e)
        {
            switch (e.Key)   //I KeyEventArgs finns värdet "Key" som definierar vilken knapp som har tryckts ner, typ P, R, 8, Enter, NumLock etc. Switchar på det och sätter bool till true
            {
                case Key.J:        //här kan man ange typ vilka tangenter som helst, det ändrar kontrollen till spelet. Kanske ha userInput så man kan ändra själv?
                    _leftButtonIsDown = true;
                    break;

                case Key.L:
                    _rightButtonIsDown = true;
                    break;

                case Key.I:
                    _upButtonIsDown = true;
                    break;

                case Key.K:
                    _downButtonIsDown = true;
                    break;

                default:
                    break;
            }
        }

        internal void HandleKeyUp(KeyEventArgs e)
        {
            switch (e.Key)   //I KeyEventArgs finns värdet "Key" som helt enkelt är vilken knapp som har tryckts ner, typ P, R, 8, Enter, NumLock etc. Switchar på det och sätter bool till true
            {
                case Key.J:        //här kan man ange typ vilka tangenter som helst, det ändrar kontrollen till spelet. Kanske ha userInput så man kan ändra själv?
                    _leftButtonIsDown = false;
                    break;

                case Key.L:
                    _rightButtonIsDown = false;
                    break;

                case Key.I:
                    _upButtonIsDown = false;
                    break;

                case Key.K:
                    _downButtonIsDown = false;
                    break;

                default:
                    break;
            }
        }

        private void LoadKrakenImageProcessing()
        {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri("pack://application:,,,/SUP23_G9;component/Views/Components/Images/Happy_Kraken_Left.bmp");
            image.DecodePixelWidth = 50;
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.EndInit();

            PlayerImage = image;
        }

        private void TurnSpriteHorizontally() => FlipImageX = -1.0;

        private void TurnSpriteBack() => FlipImageX = 1.0;
    }
}
