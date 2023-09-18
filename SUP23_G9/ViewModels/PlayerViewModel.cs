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
        #region Field-variabler
        /// <summary>
        /// Sätter spelarens minimala avstånd från spelplanens kant
        /// </summary>
        private readonly double _distanceToEdge = 5;
        /// <summary>
        /// Sätter spelarkaraktärens hastighet
        /// </summary>
        private readonly double _playerSpeed = 5;

        private bool _leftButtonIsDown, _rightButtonIsDown, _upButtonIsDown, _downButtonIsDown;

        /// <summary>
        /// Timer för att flytta spelaren med jämna interval
        /// </summary>
        DispatcherTimer _timer;
        #endregion

        #region Konstruktor
        /// <summary>
        /// Konstruktor
        /// </summary>
        public PlayerViewModel()
        {
            LeftCoordinates = 475;
            TopCoordinates = 450;
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
            //UpKeyDownCommand = new RelayCommand(x => MovePlayerUp(), x => IsNotAtTopEdge());
            //DownKeyDownCommand = new RelayCommand(x => MovePlayerDown(), x => IsNotAtBottomEdge());
            //LeftKeyDownCommand = new RelayCommand(x => MovePlayerLeft(), x => IsNotAtLeftEdge());
            //RightKeyDownCommand = new RelayCommand(x => MovePlayerRight(), x => IsNotAtRightEdge());
        }
        #endregion

        #region Properties
        /// <summary>
        /// X-koordinater för spelare
        /// </summary>
        public double LeftCoordinates { get; private set; }
        /// <summary>
        /// Y-koordinater för spelare
        /// </summary>
        public double TopCoordinates { get; private set; }
        /// <summary>
        /// Spelarens bredd
        /// </summary>
        public int Width { get; private set; }
        /// <summary>
        /// Spelarens höjd
        /// </summary>
        public int Height { get; private set; }
        /// <summary>
        /// Bild för spelarens karaktär
        /// </summary>
        public BitmapImage PlayerImage { get; private set; }
        /// <summary>
        /// Sätter vilket håll karaktärsbilden är vänd mot i horisontellt led
        /// </summary>
        public double FlipImageX { get; private set; }
        //public ICommand UpKeyDownCommand { get; private set; }
        //public ICommand DownKeyDownCommand { get; private set; }
        //public ICommand LeftKeyDownCommand { get; private set; }
        //public ICommand RightKeyDownCommand { get; private set; } 
        #endregion

        #region Event handlers
        /// <summary>
        /// Event handler för att förflytta spelaren
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Event handler för att kontrollera om en viss tangent trycks ner, här sätts med vilka tangenter spelaren styr sin karaktär
        /// </summary>
        /// <param name="e"></param>
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
        /// <summary>
        /// Event handler för att kontrollera om en viss tangent släpps, här sätts med vilka tangenter spelaren styr sin karaktär
        /// </summary>
        /// <param name="e"></param>
        internal void HandleKeyUp(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.J:
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
        #endregion

        #region Förflyttningsmetoder
        /// <summary>
        /// Flyttar spelaren åt vänster
        /// </summary>
        public void MovePlayerLeft() => LeftCoordinates -= _playerSpeed;
        /// <summary>
        /// Flyttar spelaren åt höger
        /// </summary>
        public void MovePlayerRight() => LeftCoordinates += _playerSpeed;
        /// <summary>
        /// Flyttar spelaren uppåt
        /// </summary>
        public void MovePlayerUp() => TopCoordinates -= _playerSpeed;
        /// <summary>
        /// Flyttar spelaren nedåt
        /// </summary>
        public void MovePlayerDown() => TopCoordinates += _playerSpeed;
        /// <summary>
        /// Kontrollerar om spelaren är för nära vänstra kanten av spelplanen
        /// </summary>
        /// <returns> bool </returns>
        public bool IsNotAtLeftEdge() => LeftCoordinates > _distanceToEdge;
        /// <summary>
        /// Kontrollerar om spelaren är för nära högra kanten av spelplanen
        /// </summary>
        /// <returns> bool </returns>
        public bool IsNotAtRightEdge() => (LeftCoordinates + Width) < (985 - _distanceToEdge);
        /// <summary>
        /// Kontrollerar om spelaren är för nära övre kanten av spelplanen
        /// </summary>
        /// <returns> bool </returns>
        public bool IsNotAtTopEdge() => TopCoordinates > _distanceToEdge;
        /// <summary>
        /// Kontrollerar om spelaren är för nära nedre kanten av spelplanen
        /// </summary>
        /// <returns> bool </returns>
        public bool IsNotAtBottomEdge() => (TopCoordinates + Height) < (565 - _distanceToEdge);
        #endregion

        #region Bildprocesseringsmetoder
        /// <summary>
        /// Processerar och cachar bild för spelarens karaktär
        /// </summary>
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
        /// <summary>
        /// Vänder spelarens karaktärsbild åt andra hållet i x-led
        /// </summary>
        private void TurnSpriteHorizontally() => FlipImageX = -1.0;
        /// <summary>
        /// Vänder spelarens karaktärsbild tillbaka
        /// </summary>
        private void TurnSpriteBack() => FlipImageX = 1.0; 
        #endregion
    }
}