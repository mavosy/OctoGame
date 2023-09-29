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
            LeftCoordinates = 475;
            TopCoordinates = 450;

            Width = width;
            Height = height;

            SetPlayerTimer();

            LoadImageProcessing();
            FlipImageX = 1.0;
        }
        /// <summary>
        /// X-koordinater för objekt av typen PlayerViewModel
        /// </summary>
        public double LeftCoordinates { get; private set; }
        /// <summary>
        /// Y-koordinater för objekt av typen PlayerViewModel
        /// </summary>
        public double TopCoordinates { get; private set; }
        /// <summary>
        /// Breddmått för objekt av typen PlayerViewModel, värde hämtas från konstanten "width".
        /// </summary>
        public int Width { get; private set; }
        /// <summary>
        /// Höjdmått för objekt av typen PlayerViewModel, värde hämtas från konstanten "height".
        /// </summary>
        public int Height { get; private set; }
        /// <summary>
        /// Mått på den renderade bredden för programfönstret. Sätts från MainViewModel. Används för att begränsa spelarens rörelser till inom skärmen
        /// </summary>
        public double WindowWidth { get; set; }
        /// <summary>
        /// Mått på den renderade höjden för programfönstret. Sätts från MainViewModel. Används för att begränsa spelarens rörelser till inom skärmen
        /// </summary>
        public double WindowHeight { get; set; }
        /// <summary>
        /// Bild som representerar objekt av typen PlayerViewModel i gränssnittet
        /// </summary>
        public BitmapImage SpriteImage { get; private set; }
        /// <summary>
        /// Bestämmer den visuella riktningen på bilden som representerar objekt av typen PlayerViewModel i gränssnittet. Värde 1.0 är originalvärdet, -1.0 ger en spegelvänd bild.
        /// </summary>
        public double FlipImageX { get; private set; }
        /// <summary>
        /// Etablerar och konfigurerar timer för rörelse av objekt av typen PlayerViewModel
        /// </summary>
        private void SetPlayerTimer()
        {
            _playerTimer = new DispatcherTimer();
            _playerTimer.Interval = TimeSpan.FromMilliseconds(playerUpdateFrequencyInMilliseconds);
        }
        /// <summary>
        /// Startar timer för rörelse objekt av typen PlayerViewModel och fäster eventet MovePlayerEvent att köras vid givet intervall
        /// </summary>
        public void StartPlayerTimer()
        {
            _playerTimer.Tick += MovePlayerEvent;
            _playerTimer.Start();
        }
        /// <summary>
        /// Stoppar timer för rörelse av objekt av typen PlayerViewModel och lossar eventet MovePlayerEvent
        /// </summary>
        public void StopPlayerTimer()
        {
            _playerTimer.Tick -= MovePlayerEvent;
            _playerTimer.Stop();
        }
        /// <summary>
        /// Förflyttar objekt av typen PlayerViewModel i given riktining om objektet har tillåtelse. Körs av timer vid givet intervall
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MovePlayerEvent(object? sender, EventArgs e)
        {
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
        /// Här och i "HandleKeyUp()" sätts med vilka tangenter objekt av typen PlayerViewModel styrs, och kontrollerar om de är nertryckta. Event-argument skickas från händelseevent i Player.xaml.cs
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
        /// Här och i "HandleKeyDown()" sätts med vilka tangenter objekt av typen PlayerViewModel styrs, och kontrollerar om man släppt tangenten. Event-argument skickas från händelseevent i Player.xaml.cs
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
        /// <summary>
        /// Förflyttar objekt av typen PlayerViewModel åt vänster genom att ändra objektets faktiska koordinatvärde med hjälp av konstanten _playerSpeed
        /// </summary>
        public void MovePlayerLeft()
        {
            LeftCoordinates -= playerSpeed;
        }
        /// <summary>
        /// Förflyttar objekt av typen PlayerViewModel åt höger genom att ändra objektets faktiska koordinatvärde med hjälp av konstanten _playerSpeed
        /// </summary>
        public void MovePlayerRight()
        {
            LeftCoordinates += playerSpeed;
        }
        /// <summary>
        /// Förflyttar objekt av typen PlayerViewModel uppåt genom att ändra objektets faktiska koordinatvärde med hjälp av konstanten _playerSpeed
        /// </summary>
        public void MovePlayerUp()
        {
            TopCoordinates -= playerSpeed;
        }
        /// <summary>
        /// Förflyttar objekt av typen PlayerViewModel nedåt genom att ändra objektets faktiska koordinatvärde med hjälp av konstanten _playerSpeed
        /// </summary>
        public void MovePlayerDown()
        {
            TopCoordinates += playerSpeed;
        }
        /// <summary>
        /// Kontrollerar om ett objekt av typen PlayerViewModel är tillräckligt långt bort från den vänstra kanten. Förhindrar objektet från att röra sig utanför skärmen
        /// </summary>
        /// <returns>bool</returns>
        public bool IsNotAtLeftEdge()
        {
            return LeftCoordinates > distanceToEdge;
        }
        /// <summary>
        /// Kontrollerar om ett objekt av typen PlayerViewModel är tillräckligt långt bort från den högra kanten. Förhindrar objektet från att röra sig utanför skärmen
        /// </summary>
        /// <returns>bool</returns>
        public bool IsNotAtRightEdge()
        {
            return (LeftCoordinates + Width) < (WindowWidth - distanceToEdge);
        }
        /// <summary>
        /// Kontrollerar om ett objekt av typen PlayerViewModel är tillräckligt långt bort från den övre kanten. Förhindrar objektet från att röra sig utanför skärmen
        /// </summary>
        /// <returns>bool</returns>
        public bool IsNotAtTopEdge()
        {
            return TopCoordinates > distanceToEdge;
        }
        /// <summary>
        /// Kontrollerar om ett objekt av typen PlayerViewModel är tillräckligt långt bort från den nedre kanten. Förhindrar objektet från att röra sig utanför skärmen
        /// </summary>
        /// <returns>bool</returns>
        public bool IsNotAtBottomEdge()
        {
            return (TopCoordinates + Height) < (WindowHeight - distanceToEdge);
        }

        /// <summary>
        /// Processerar bild till lämpligt format för bild tillhörande objekt av typen ShipViewModel, samt tilldelar bild till egenskapen SpriteImage
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
        /// <summary>
        /// Spegelvänder bilden som representerar objekt av typen PlayerViewModel
        /// </summary>
        private void TurnSpriteHorizontally()
        {
            FlipImageX = -1.0;
        }
        /// <summary>
        /// Vänder bilden som representerar objekt av typen PlayerViewModel till originalhåll
        /// </summary>
        private void TurnSpriteBack()
        {
            FlipImageX = 1.0;
        }
    }
}