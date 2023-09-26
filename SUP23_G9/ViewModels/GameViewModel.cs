using SUP23_G9.ViewModels.Base;
using SUP23_G9.Views;
using SUP23_G9.Views.Characters;
using SUP23_G9.Views.Components;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using static System.Formats.Asn1.AsnWriter;
using static System.Net.Mime.MediaTypeNames;

namespace SUP23_G9.ViewModels
{
    public class GameViewModel : BaseViewModel
    {
        #region Fields and constants
        public DispatcherTimer _gameTimer;
        private bool _isGameActive = true;   // Nytt boolskt fält som indikerar om spelet är aktivt eller inte. //TODO Behövs denna?

        private readonly int _speedShip = 4; //TODO Om vi inte behöver ändra på dessa värden under spelets gång så kanske vi borde göra dem till constants.
        private readonly int _speedObstacle = 2; //Det gäller alla fasta fieldvariabler tror jag
        
        private static readonly Random _random = new();
        private double _mainWindowHeight = System.Windows.Application.Current.MainWindow.ActualHeight;   //TODO Skulle behöva se över dessa och hitta ett MVVM-sätt att hämta höjd och bredd
        private double _mainWindowWidth = System.Windows.Application.Current.MainWindow.ActualWidth;

        BitmapImage _fullHeart;
        BitmapImage _emptyHeart;
        #endregion

        public GameViewModel()
        {
            Ships = new ObservableCollection<ShipViewModel>();
            Obstacles = new ObservableCollection<ObstacleViewModel>();

            CreateRandomShips();
            CreateRandomObstacles();

            PlayerHealth = 3;
            LoadEmptyHeartImageProcessing();
            LoadFullHeartImageProcessing();
            Heart1 = _fullHeart;
            Heart2 = _fullHeart;
            Heart3 = _fullHeart;

            PlayerVM = new PlayerViewModel();

            //StartMovingObject();
            SetGameTimer();
            CountdownTimer.TimeUp += CountdownTimer_TimeUp;

            Debug.WriteLine($"New GameViewModel with ID: {InstanceID}");
        }

        #region Properties
        public int PointResult { get; set; }
        public Points GamePoints { get; } = new Points();
        public TimerViewModel CountdownTimer { get; set; } = new TimerViewModel(10); // Startar med 1 min.
        public PlayerViewModel PlayerVM { get; set; }
        public ObservableCollection<ShipViewModel> Ships { get; set; }
        public ObservableCollection<ObstacleViewModel> Obstacles { get; set; }
        public int PlayerHealth { get; set; }
        public BitmapImage Heart1 { get; set; }
        public BitmapImage Heart2 { get; set; }
        public BitmapImage Heart3 { get; set; } 
        #endregion

        private void CreateRandomShips()
        {
            for (int i = 0; i < 15; i++) // Skapa 10 skepp
            {
                int randomTop;
                int randomLeft;
                ShipViewModel newShip;

                do
                {
                    randomTop = GenerateRandomTop() - 600;
                    randomLeft = GenerateRandomLeft();
                    newShip = new ShipViewModel { Top = randomTop, Left = randomLeft };
                }
                while (NewShipCollidesWithExistingShips(newShip)); //Sålänge (while) som villkoret är true så kommer koden i do köras - körs tills fått rätt värden som ej krockar.
                                                                   //inspo från battleship videos del 6 ca 13:30

                Ships.Add(newShip); //Om false så läggs de nya skeppen till
            }
        }

        private void CreateRandomObstacles()
        {
            for (int i = 0; i < 5; i++) //Obstacles 5st
            {
                int randomTop = GenerateRandomTop() - 600;
                int randomLeft = GenerateRandomLeft();
                Obstacles.Add(new ObstacleViewModel { Top = randomTop, Left = randomLeft });
            }
        }

        private int GenerateRandomTop() => _random.Next((int)_mainWindowHeight);
        private int GenerateRandomLeft() => _random.Next((int)_mainWindowWidth); 

        //private void StartMovingObject()
        //{
        //    _gameTimer = new DispatcherTimer();
        //    Debug.WriteLine($"Setting up GameViewModel with ID: {InstanceID}");
        //    _gameTimer.Interval = TimeSpan.FromMilliseconds(20);
        //    _gameTimer.Tick += GameTimerEvent;
        //    _gameTimer.Start();
        //}

        private void SetGameTimer()
        {
            _gameTimer = new DispatcherTimer();
            _gameTimer.Interval = TimeSpan.FromMilliseconds(20);
        }

        public void GameTimerEvent(object sender, EventArgs e)
        {
            MoveShipsLoop();
            MoveObstaclesLoop();
            SetPlayerShipCollisionConsequence();
            SetPlayerObstacleCollisionConsequence();
            Debug.WriteLine($"GameViewModel event fire with ID: {InstanceID}");
        }

        /// <summary>
        /// Loopar genom skeppen för att hitta vilka som "ramlar" ut ur fönstret för att sedan repositionera till -10
        /// </summary>
        private void MoveShipsLoop()
        {
            foreach (ShipViewModel ship in Ships)
            {
                ship.Top += _speedShip;

                if (ship.Top > _mainWindowHeight)
                {
                    ResetShipPosition(ship);
                }
            }
        }

        /// <summary>
        /// Loopar genom hinder för att hitta vilka som "ramlar" ut ur fönstret för att sedan repositionera till 0
        /// </summary>
        private void MoveObstaclesLoop()
        {
            foreach (ObstacleViewModel obstacle in Obstacles)
            {
                obstacle.Top += _speedObstacle;

                if (obstacle.Top > _mainWindowHeight)
                {
                    obstacle.Top = 0;
                    obstacle.Left = GenerateRandomLeft();
                }
            }
        } 

        /// <summary>
        /// Kontrollerar att objekten av typen ShipViewModel ej kolliderar med existerande objekt i canvas vid repositionering, om true så genereras nytt värde 
        /// </summary>
        private void ResetShipPosition(ShipViewModel ship)
        {
            int newLeft = GenerateRandomLeft();

            while (IsCollisionWithExistingShips(newLeft, -10))
            {
                newLeft = GenerateRandomLeft();
            }

            ship.Top = 0;
            ship.Left = newLeft;
        }

        /// <summary>
        /// Kontrollerar att objekten av typen ShipViewModel ej kolliderar med existerande objekt i canvas vid repositionering
        /// </summary>
        private bool IsCollisionWithExistingShips(int left, int top)
        {
            foreach (var existingShip in Ships) //Måste göra ny kollisonskoll mellan existerande skepp och det nya värdet som genereras
            {
                bool collisionX = left < existingShip.Left + 55 && left + 55 > existingShip.Left;
                bool collisionY = top < existingShip.Top + 55 && top + 55 > existingShip.Top;

                if (collisionX && collisionY)
                {
                    return true;
                }
            }

            return false;
        }

        private bool NewShipCollidesWithExistingShips(ShipViewModel newShip)
        {
            foreach (var existingShip in Ships)
            {
                bool collisionX = newShip.Left < existingShip.Left + 55 && newShip.Left + 55 > existingShip.Left; //Skrev 55 för att få lite extra marginal
                bool collisionY = newShip.Top < existingShip.Top + 55 && newShip.Top + 55 > existingShip.Top;

                if (collisionX && collisionY)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Kontrollerar att objekten av typen ShipViewModel ej kolliderar med existerande objekt i canvas vid repositionering efter att spelaren krockar med ett skepp
        /// </summary>
        public void SetPlayerShipCollisionConsequence()
        {
            if (!_isGameActive) return;

            foreach (var ship in Ships)
            {
                if (PlayerCollidesWithShip(ship))
                {
                    PlayerShipCollision(ship);
                }
            }
        }

        //TODO Föreslår namnbyte till något tydligare
        /// <summary>
        /// Kontrollerar att objekten av typen ShipViewModel ej kolliderar med existerande objekt i canvas vid repositionering efter att spelaren krockar med ett skepp
        /// </summary>
        private void PlayerShipCollision(ShipViewModel ship)
        {
            int newLeft = GenerateRandomLeft();

            while (IsCollisionWithExistingShips(newLeft, 0))
            {
                newLeft = GenerateRandomLeft();
            }

            ship.Top = 0;
            ship.Left = newLeft;
            GamePoints.AddPoints(10);
        }

        private void SetPlayerObstacleCollisionConsequence()
        {
            if (!_isGameActive) return; // Om spelet inte är aktivt, gör ingenting och avbryt metoden.

            foreach (var obstacle in Obstacles)
            {
                if (PlayerCollidesWithObstacle(obstacle))
                {
                    obstacle.Top = 0;
                    obstacle.Left = GenerateRandomLeft();

                    GamePoints.DeductPoints(5);
                    PlayerDamaged();
                    PointResult -= 5;
                }
            }
        }

        private bool PlayerCollidesWithShip(ShipViewModel ship)
        {
            bool collisionX = ship.Left < GlobalVariabels._playerCoordinatesLeft + 50 && ship.Left + 50 > GlobalVariabels._playerCoordinatesLeft;
            bool collisionY = ship.Top < GlobalVariabels._playerCoordinatesTop + 50 && ship.Top + 50 > GlobalVariabels._playerCoordinatesTop;

            if (collisionX && collisionY)
            {
                return true;
            }
            return false;
        }

        public bool PlayerCollidesWithObstacle(ObstacleViewModel obstacle)
        {
            bool collisionX = obstacle.Left < GlobalVariabels._playerCoordinatesLeft + 50 && obstacle.Left + 50 > GlobalVariabels._playerCoordinatesLeft;
            bool collisionY = obstacle.Top < GlobalVariabels._playerCoordinatesTop + 50 && obstacle.Top + 50 > GlobalVariabels._playerCoordinatesTop;

            if (collisionX && collisionY)
            {
                return true;
            }
            return false;
        }

        //TODO denna kan nog delas upp i mindre metoder
        /// <summary>
        /// Metod för om spelaren skadas eller dör
        /// </summary>
        public void PlayerDamaged()
        {
            PlayerHealth -= 1;
            if (PlayerHealth <= 2)
            {
                Heart3 = _emptyHeart;
            }
            if (PlayerHealth <= 1)
            {
                Heart2 = _emptyHeart;
            }
            if (PlayerHealth <= 0)
            {
                OpenGameOverView();
            }
        }

        //public void StartGame()
        //{
        //    _isGameActive = true;
        //}

        //public void StopGame()
        //{
        //    _isGameActive = false;
        //}

        public void StartTimers()
        {
            StartGameTimer();
            StartCountdownTimer();
            PlayerVM.StartPlayerTimer();
            //RaiseStartPlayerTimerDelegate();
        }

        public void StopTimers()
        {
            StopGameTimer();
            StopCountdownTimer();
            PlayerVM.StopPlayerTimer();
            //RaiseStopPlayerTimerDelegate();
        }

        public void StartGameTimer()
        {
            _gameTimer.Tick += GameTimerEvent;
            _gameTimer.Start();
        }
        public void StopGameTimer()
        {
            _gameTimer.Tick -= GameTimerEvent;
            _gameTimer.Stop();
        }

        public void StartCountdownTimer()
        {
            CountdownTimer._timer.Start();
        }
        public void StopCountdownTimer()
        {
            CountdownTimer._timer.Tick += CountdownTimer.TimerTick;
            CountdownTimer._timer.Stop();
        }

        public Action StartPlayerTimerDelegate;
        public Action StopPlayerTimerDelegate;
        public void RaiseStartPlayerTimerDelegate() => StartPlayerTimerDelegate?.Invoke();
        public void RaiseStopPlayerTimerDelegate() => StopPlayerTimerDelegate?.Invoke();


        private void CountdownTimer_TimeUp(object sender, EventArgs e)
        {
            OpenGameOverView();
        }
      
        public Action<int> SwitchToGameOverViewEvent { get; set; }
        public void RaiseSwitchToGameOverViewEvent(int finalScore) => SwitchToGameOverViewEvent?.Invoke(finalScore);
     
        public void OpenGameOverView()
        {
            int finalScore = GamePoints.GetScore();
           
            if (finalScore <= 0) 
            {
                GameOverEvent?.Invoke(finalScore);
            }

            RaiseSwitchToGameOverViewEvent(finalScore);
           
            var gameOverViewModel = new GameOverViewModel(finalScore);
        }

        public event Action<int> GameOverEvent;

        private void LoadFullHeartImageProcessing()
        {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri("pack://application:,,,/SUP23_G9;component/Views/Components/Images/LifeHeartFull.bmp");
            image.DecodePixelWidth = 50;
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.EndInit();

            _fullHeart = image;
        }

        private void LoadEmptyHeartImageProcessing()
        {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri("pack://application:,,,/SUP23_G9;component/Views/Components/Images/LifeHeartEmpty.bmp");
            image.DecodePixelWidth = 50;
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.EndInit();

            _emptyHeart = image;
        }
    }
}