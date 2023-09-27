using SUP23_G9.ViewModels.Base;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace SUP23_G9.ViewModels
{
    public class GameViewModel : BaseViewModel
    {
        #region Fields and Constants
        private DispatcherTimer _gameTimer;
        private DispatcherTimer _increaseObstaclesTimer;

        private int _secondsPassedCounter = 0;
        private int _speedShip = 4;
        private int _speedObstacle = 2;

        private static readonly Random _random = new();
        private double _mainWindowHeight = System.Windows.Application.Current.MainWindow.ActualHeight;   //TODO Skulle behöva se över dessa och hitta ett MVVM-sätt att hämta höjd och bredd
        private double _mainWindowWidth = System.Windows.Application.Current.MainWindow.ActualWidth;

        private BitmapImage _fullHeart;
        private BitmapImage _emptyHeart;
        #endregion

        public GameViewModel()
        {
            Ships = new ObservableCollection<ShipViewModel>();
            Obstacles = new ObservableCollection<ObstacleViewModel>();
            GamePoints = new Points();

            CreateRandomShips();
            CreateRandomObstacles();

            PlayerHealth = 3;
            LoadEmptyHeartImageProcessing();
            LoadFullHeartImageProcessing();
            Heart1 = _fullHeart;
            Heart2 = _fullHeart;
            Heart3 = _fullHeart;

            PlayerVM = new PlayerViewModel();

            SetGameTimer();
            SetIncreaseObstaclesTimer();
            SetCountdownTimer(60);

            Debug.WriteLine($"New GameViewModel with ID: {InstanceID}");
        }

        #region Properties
        public int PointResult { get; set; }
        public Points GamePoints { get; }
        public TimerViewModel CountdownTimer { get; set; }
        public PlayerViewModel PlayerVM { get; set; }
        public ObservableCollection<ShipViewModel> Ships { get; set; }
        public ObservableCollection<ObstacleViewModel> Obstacles { get; set; }
        public int PlayerHealth { get; set; }
        public BitmapImage Heart1 { get; set; }
        public BitmapImage Heart2 { get; set; }
        public BitmapImage Heart3 { get; set; }
        #endregion

        #region Creation of objects and coordinates

        /// <summary>
        /// Event som körs 1 gång i sekunden.
        /// </summary>
        private void CountdownTimer_IncreaseObstacles(object? sender, EventArgs e)
        {
            _secondsPassedCounter++;
            IncrementalDifficultyIncrease();
        }

        /// <summary>
        /// Ökar svårighetsgrad efter att ett givet antal sekunder har passerat
        /// </summary>
        private void IncrementalDifficultyIncrease()
        {
            if (HasSecondsPassed(10))
            {
                CreateRandomObstacles();
                IncreaseObstacleSpeed();
                IncreaseShipSpeed();
            }
        }

        /// <summary>
        /// Avgör om ett givet antal sekunder har passerat sedan senaste ökningen
        /// </summary>
        /// <param name="secToDifficultyIncrease"></param>
        /// <returns></returns>
        private bool HasSecondsPassed(int secToDifficultyIncrease)
        {
            return _secondsPassedCounter % secToDifficultyIncrease == 0;
        }

        /// <summary>
        /// Ökar alla hinders hastighet
        /// </summary>
        private void IncreaseObstacleSpeed()
        {
            _speedObstacle++;
        }

        /// <summary>
        /// Ökar alla skepps hastighet
        /// </summary>
        private void IncreaseShipSpeed()
        {
            _speedShip++;
        }

        /// <summary>
        /// Skapar nya objekt av typen ShipViewModel
        /// </summary>
        private void CreateRandomShips()
        {
            for (int i = 0; i < 20; i++) // Skapa 10 skepp
            {
                int randomTop;
                int randomLeft;
                ShipViewModel newShip;

                do
                {
                    randomTop = GenerateRandomTop() - 600;
                    randomLeft = GenerateRandomLeft();
                    newShip = new ShipViewModel { TopCoordinates = randomTop, LeftCoordinates = randomLeft };
                }
                while (NewShipCollidesWithExistingShips(newShip)); //Sålänge (while) som villkoret är true så kommer koden i do köras - körs tills fått rätt värden som ej krockar.
                                                                   //inspo från battleship videos del 6 ca 13:30

                Ships.Add(newShip); //Om false så läggs de nya skeppen till
            }
        }

        private void CreateRandomObstacles()
        {
            for (int i = 0; i < 3; i++) //Obstacles 2st
            {
                int randomTop = GenerateRandomTop() - 600;
                int randomLeft = GenerateRandomLeft();
                Obstacles.Add(new ObstacleViewModel { TopCoordinates = randomTop, LeftCoordinates = randomLeft });
            }
        }

        private int GenerateRandomTop()
        {
            return _random.Next((int)_mainWindowHeight);
        }
        private int GenerateRandomLeft()
        {
            return _random.Next((int)_mainWindowWidth);
        }

        #endregion

        #region Movement and collision
        /// <summary>
        /// Loopar genom skeppen för att hitta vilka som "ramlar" ut ur fönstret för att sedan repositionera till -10
        /// </summary>
        private void MoveShipsLoop()
        {
            foreach (ShipViewModel ship in Ships)
            {
                ship.TopCoordinates += _speedShip;

                if (ship.TopCoordinates > _mainWindowHeight)
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
                obstacle.TopCoordinates += _speedObstacle;

                if (obstacle.TopCoordinates > _mainWindowHeight)
                {
                    obstacle.TopCoordinates = 0;
                    obstacle.LeftCoordinates = GenerateRandomLeft();
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

            ship.TopCoordinates = 0;
            ship.LeftCoordinates = newLeft;
        }

        /// <summary>
        /// Kontrollerar att objekten av typen ShipViewModel ej kolliderar med existerande objekt i canvas vid repositionering
        /// </summary>
        private bool IsCollisionWithExistingShips(int left, int top)
        {
            foreach (var existingShip in Ships) //Måste göra ny kollisonskoll mellan existerande skepp och det nya värdet som genereras
            {
                bool collisionX = left < existingShip.LeftCoordinates + 55 && left + 55 > existingShip.LeftCoordinates;
                bool collisionY = top < existingShip.TopCoordinates + 55 && top + 55 > existingShip.TopCoordinates;

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
                bool collisionX = newShip.LeftCoordinates < existingShip.LeftCoordinates + 55 && newShip.LeftCoordinates + 55 > existingShip.LeftCoordinates; //Skrev 55 för att få lite extra marginal
                bool collisionY = newShip.TopCoordinates < existingShip.TopCoordinates + 55 && newShip.TopCoordinates + 55 > existingShip.TopCoordinates;

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

            ship.TopCoordinates = 0;
            ship.LeftCoordinates = newLeft;
            GamePoints.AddPoints(10);
        }

        private void SetPlayerObstacleCollisionConsequence()
        {
            foreach (var obstacle in Obstacles)
            {
                if (PlayerCollidesWithObstacle(obstacle))
                {
                    obstacle.TopCoordinates = 0;
                    obstacle.LeftCoordinates = GenerateRandomLeft();

                    GamePoints.DeductPoints(5);
                    PlayerDamaged();
                    PointResult -= 5;
                }
            }
        }

        private bool PlayerCollidesWithShip(ShipViewModel ship)
        {
            bool collisionX = ship.LeftCoordinates < GlobalVariabels._playerCoordinatesLeft + 50 && ship.LeftCoordinates + 50 > GlobalVariabels._playerCoordinatesLeft;
            bool collisionY = ship.TopCoordinates < GlobalVariabels._playerCoordinatesTop + 50 && ship.TopCoordinates + 50 > GlobalVariabels._playerCoordinatesTop;

            if (collisionX && collisionY)
            {
                return true;
            }
            return false;
        }

        public bool PlayerCollidesWithObstacle(ObstacleViewModel obstacle)
        {
            bool collisionX = obstacle.LeftCoordinates < GlobalVariabels._playerCoordinatesLeft + 50 && obstacle.LeftCoordinates + 50 > GlobalVariabels._playerCoordinatesLeft;
            bool collisionY = obstacle.TopCoordinates < GlobalVariabels._playerCoordinatesTop + 50 && obstacle.TopCoordinates + 50 > GlobalVariabels._playerCoordinatesTop;

            if (collisionX && collisionY)
            {
                return true;
            }
            return false;
        }
        #endregion

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

        #region Timers and Game ending
        public void GameTimerEvent(object sender, EventArgs e)
        {
            MoveShipsLoop();
            MoveObstaclesLoop();
            SetPlayerShipCollisionConsequence();
            SetPlayerObstacleCollisionConsequence();
            Debug.WriteLine($"GameViewModel event fire with ID: {InstanceID}");
        }

        private void CountdownTimer_TimeUp(object sender, EventArgs e)
        {
            OpenGameOverView();
        }

        public void StartTimers()
        {
            StartGameTimer();
            StartCountdownTimer();
            StartIncreaseObstaclesTimer();
            PlayerVM.StartPlayerTimer();
        }

        public void StopTimers()
        {
            StopGameTimer();
            StopCountdownTimer();
            StopIncreaseObstaclesTimer();
            PlayerVM.StopPlayerTimer();
        }

        private void SetIncreaseObstaclesTimer()
        {
            _increaseObstaclesTimer = new DispatcherTimer();
            _increaseObstaclesTimer.Interval = TimeSpan.FromSeconds(1);
        }

        private void StartIncreaseObstaclesTimer()
        {
            _increaseObstaclesTimer.Tick += CountdownTimer_IncreaseObstacles;
            _increaseObstaclesTimer.Start();
        }
        private void StopIncreaseObstaclesTimer()
        {
            _increaseObstaclesTimer.Tick -= CountdownTimer_IncreaseObstacles;
            _increaseObstaclesTimer.Stop();
        }

        private void SetGameTimer()
        {
            _gameTimer = new DispatcherTimer();
            _gameTimer.Interval = TimeSpan.FromMilliseconds(20);
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
        public void SetCountdownTimer(int seconds)
        {
            CountdownTimer = new TimerViewModel(seconds); // Startar med 1 min.
            CountdownTimer.TimeUpHandler += CountdownTimer_TimeUp;
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

        public Action<int> SetToGameOverViewHandler;
        public event Action<int> GameOverEvent;
        public void RaiseSetToGameOverViewHandler(int finalScore)
        {
            SetToGameOverViewHandler?.Invoke(finalScore);
        }

        public void OpenGameOverView()
        {
            int finalScore = GamePoints.GetScore();

            if (finalScore <= 0)
            {
                GameOverEvent?.Invoke(finalScore);
            }

            RaiseSetToGameOverViewHandler(finalScore);

            var gameOverViewModel = new GameOverViewModel(finalScore); //TODO Behövs denna? Verkar inte leda någonstans, den lokala variabeln används inte
        }

        #endregion

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