using SUP23_G9.ViewModels.Base;
using System;
using System.Diagnostics;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Threading;

namespace SUP23_G9.ViewModels
{
    public class PlayerViewModel : BaseViewModel
    {
        private const double distanceToEdge = 5;
        private const double playerSpeed = 5;

        public const int width = 50;
        public const int height = 50;

        private const int playerUpdateFrequencyInMilliseconds = 10;
        private bool _leftButtonIsDown, _rightButtonIsDown, _upButtonIsDown, _downButtonIsDown;

        DispatcherTimer _playerTimer;

        public PlayerViewModel()
        {
            //Debug.WriteLine($"PlayerViewModel width: {windowWidth}, height: {windowHeight}");
            LeftCoordinates = 475;
            TopCoordinates = 450;

            Width = width;
            Height = height;

            SetPlayerTimer();

            LoadImageProcessing();
            FlipImageX = 1.0;
            //Debug.WriteLine($"New playerViewModel with ID: {InstanceID}");
        }

        public double LeftCoordinates { get; private set; }
        public double TopCoordinates { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public double WindowWidth { get; set; }
        public double WindowHeight { get; set; }
        public BitmapImage SpriteImage { get; private set; }
        public double FlipImageX { get; private set; }

        private void SetPlayerTimer()
        {
            _playerTimer = new DispatcherTimer();
            _playerTimer.Interval = TimeSpan.FromMilliseconds(playerUpdateFrequencyInMilliseconds);
        }

        public void StartPlayerTimer()
        {
            _playerTimer.Tick += MovePlayerEvent;
            _playerTimer.Start();
        }

        public void StopPlayerTimer()
        {
            _playerTimer.Tick -= MovePlayerEvent;
            _playerTimer.Stop();
        }

        private void MovePlayerEvent(object? sender, EventArgs e)
        {
            //Debug.WriteLine($"PlayerViewModel event fire with ID: {InstanceID}");
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
        /// Här och i "HandleKeyUp()" sätts med vilka tangenter spelaren styr sin karaktär
        /// </summary>
        /// <param name="e"></param>
        internal void HandleKeyDown(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.J:
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
        /// Här och i "HandleKeyDown()" sätts med vilka tangenter spelaren styr sin karaktär
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

        public void MovePlayerLeft()
        {
            LeftCoordinates -= playerSpeed;
        }

        public void MovePlayerRight()
        {
            LeftCoordinates += playerSpeed;
        }

        public void MovePlayerUp()
        {
            TopCoordinates -= playerSpeed;
        }

        public void MovePlayerDown()
        {
            TopCoordinates += playerSpeed;
        }

        public bool IsNotAtLeftEdge()
        {
            return LeftCoordinates > distanceToEdge;
        }

        public bool IsNotAtRightEdge()
        {
            return (LeftCoordinates + Width) < (WindowWidth - distanceToEdge);
        }

        public bool IsNotAtTopEdge()
        {
            return TopCoordinates > distanceToEdge;
        }

        public bool IsNotAtBottomEdge()
        {
            return (TopCoordinates + Height) < (WindowHeight - distanceToEdge);
        }

        /// <summary>
        /// Laddar in, processerar och cachar bild för spelarens karaktär
        /// </summary>
        private void LoadImageProcessing()
        {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri("pack://application:,,,/SUP23_G9;component/Views/Components/Images/Happy_Kraken_Left.bmp");
            image.DecodePixelWidth = width;
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.EndInit();

            SpriteImage = image;
        }

        private void TurnSpriteHorizontally()
        {
            FlipImageX = -1.0;
        }

        private void TurnSpriteBack()
        {
            FlipImageX = 1.0;
        }
    }
}