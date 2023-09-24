using SUP23_G9.ViewModels.Base;
using System;
using System.Diagnostics;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace SUP23_G9.ViewModels
{
    public class PlayerViewModel : BaseViewModel
    {
        private const double distanceToEdge = 5;
        private const double playerSpeed = 5;

        private const int correctedGameAreaWidth = 985;
        private const int correctedGameAreaHeight = 565;

        private bool _leftButtonIsDown, _rightButtonIsDown, _upButtonIsDown, _downButtonIsDown;

        DispatcherTimer _playerTimer;

        public PlayerViewModel()
        {
            LeftCoordinates = 475;
            TopCoordinates = 450;
            Width = 50;
            Height = 50;

            _playerTimer = new DispatcherTimer();
            _playerTimer.Interval = TimeSpan.FromMilliseconds(10);

            _playerTimer.Tick += MovePlayerEvent;
            _playerTimer.Start();

            FlipImageX = 1.0;

            LoadKrakenImageProcessing();

            Debug.WriteLine($"Initializing new instance of PlayerViewModel(with timer) with ID: {InstanceID}");
        }

        /// <summary>
        /// X-koordinat för spelarens vänstra kant
        /// </summary>
        public double LeftCoordinates { get; private set; }
        /// <summary>
        /// Y-koordinat för spelarens övre kant, Y-koordinatens värde blir högre ju längre ner spelaren befinner sig
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
        /// Bild för spelarens karaktär i spelet. Binder till UI.
        /// </summary>
        public BitmapImage PlayerImage { get; private set; }
        /// <summary>
        /// Sätter vilket håll karaktärsbilden är vänd mot i horisontellt led (1.0=original, -1.0=spegel)
        /// </summary>
        public double FlipImageX { get; private set; }

        /// <summary>
        /// Event handler för att förflytta spelaren. Uppdaterar även GlobalVariabels med spelarens koordinater.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MovePlayerEvent(object? sender, EventArgs e)
        {
            UpdateGlobalVariabelsWithPlayerCoordinates();

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
        
        private void UpdateGlobalVariabelsWithPlayerCoordinates()
        {
            GlobalVariabels._playerCoordinatesLeft = LeftCoordinates;
            GlobalVariabels._playerCoordinatesTop = TopCoordinates;
        }

        /// <summary>
        /// Event handler för att kontrollera om en viss tangent är nertryckt, här och i "HandleKeyUp()" sätts med vilka tangenter spelaren styr sin karaktär
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
        /// Event handler för att kontrollera om en viss tangent släpps, här och i "HandleKeyDown()" sätts med vilka tangenter spelaren styr sin karaktär
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

        public void MovePlayerLeft() => LeftCoordinates -= playerSpeed;
        public void MovePlayerRight() => LeftCoordinates += playerSpeed;
        public void MovePlayerUp() => TopCoordinates -= playerSpeed;
        public void MovePlayerDown() => TopCoordinates += playerSpeed;
        public bool IsNotAtLeftEdge() => LeftCoordinates > distanceToEdge;
        public bool IsNotAtRightEdge() => (LeftCoordinates + Width) < correctedGameAreaWidth - distanceToEdge;
        public bool IsNotAtTopEdge() => TopCoordinates > distanceToEdge;
        public bool IsNotAtBottomEdge() => (TopCoordinates + Height) < correctedGameAreaHeight - distanceToEdge;

        /// <summary>
        /// Laddar in, processerar och cachar bild för spelarens karaktär
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
        /// Vänder spelarens karaktärsbild åt andra hållet än originalet, i x-led
        /// </summary>
        private void TurnSpriteHorizontally() => FlipImageX = -1.0;
        /// <summary>
        /// Vänder spelarens karaktärsbild tillbaka till originalhållet
        /// </summary>
        private void TurnSpriteBack() => FlipImageX = 1.0;

        public void StopPlayerTimer()
        {
            _playerTimer.Stop();
        }
    }
}