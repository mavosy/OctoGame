using SUP23_G9.ViewModels.Base;
using SUP23_G9.Views.Characters;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace SUP23_G9.ViewModels
{
    public class GameViewModel : BaseViewModel
    {
        #region Fields and Constants
        private DispatcherTimer _gameTimer;
        private DispatcherTimer _increaseDifficultyTimer;

        private static readonly int _shipWidth = ShipViewModel.width;
        private static readonly int _shipHeight = ShipViewModel.height;
        private static readonly int _playerWidth = PlayerViewModel.width;
        private static readonly int _playerHeight = PlayerViewModel.height;
        private static readonly int _obstacleWidth = ObstacleViewModel.width;
        private static readonly int _obstacleHeight = ObstacleViewModel.height;

        private int _secondsPassedCounter = 0;
        private const int secondsBetweenDifficultyIncrease = 20;

        private int _speedShip = 4;
        private int _speedObstacle = 2;
        private const int gainedPointsFromShips = 10;
        private const int lostPointsFromObstacles = 5;

        private static readonly Random _random = new();
        private const int initialYAxisDeplacement = 550;
        private const int objectPaddingAtGeneration = 5;

        private double _mainWindowHeight = System.Windows.Application.Current.MainWindow.ActualHeight;   //TODO Skulle behöva se över dessa och hitta ett MVVM-sätt att hämta höjd och bredd
        private double _mainWindowWidth = System.Windows.Application.Current.MainWindow.ActualWidth;

        private const int maxPlayerHealth = 3;
        private const int gameTimerUpdateFrequencyInMilliseconds = 20;
        private BitmapImage _fullHeart;
        private BitmapImage _emptyHeart;

        private readonly MediaPlayer _backgroundMusicPlayer = new MediaPlayer();


        #endregion

        public GameViewModel()
        {
            Ships = new ObservableCollection<ShipViewModel>();
            Obstacles = new ObservableCollection<ObstacleViewModel>();
            GamePoints = new Points();

            CreateRandomShips();
            CreateRandomObstacles();

            PlayerHealth = maxPlayerHealth;
            LoadEmptyHeartImageProcessing();
            LoadFullHeartImageProcessing();
            Heart1 = _fullHeart;
            Heart2 = _fullHeart;
            Heart3 = _fullHeart;

            PlayerVM = new PlayerViewModel();

            SetGameTimer();
            SetTimerForIncreasedDifficulty();
            SetCountdownTimer(180);
            InitializeBackgroundMusic();

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
            if (IsTimeForDifficultyIncrease())
            {
                CreateRandomObstacles();
                IncreaseObstacleSpeed();
                IncreaseShipSpeed();
            }
        }

        /// <summary>
        /// Avgör om ett givet antal sekunder har passerat sedan senaste ökningen
        /// </summary>
        /// <returns></returns>
        private bool IsTimeForDifficultyIncrease()
        {
            return _secondsPassedCounter % secondsBetweenDifficultyIncrease == 0;
        }

        /// <summary>
        /// Ökar hastigheten för alla objekt av typen ObstacleViewModel
        /// </summary>
        private void IncreaseObstacleSpeed()
        {
            _speedObstacle++;
        }

        /// <summary>
        /// Ökar hastigheten för alla objekt av typen ShipViewModel
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
            for (int i = 0; i < 20; i++)
            {
                int randomTop;
                int randomLeft;
                ShipViewModel newShip;
                
                do
                {
                    randomTop = GenerateRandomTop() - initialYAxisDeplacement;
                    randomLeft = GenerateRandomLeft();
                    newShip = new ShipViewModel { TopCoordinates = randomTop, LeftCoordinates = randomLeft };
                }
                while (IsCollisionWithExistingShips(randomLeft, randomTop)); 

                Ships.Add(newShip);
            }
        }

        /// <summary>
        /// Skapar nya objekt av typen ObstacleViewModel
        /// </summary>
        private void CreateRandomObstacles()
        {
            for (int i = 0; i < 3; i++) 
            {
                int randomTop = GenerateRandomTop() - initialYAxisDeplacement;
                int randomLeft = GenerateRandomLeft();
                Obstacles.Add(new ObstacleViewModel { TopCoordinates = randomTop, LeftCoordinates = randomLeft });
            }
        }

        /// <summary>
        /// Randomiserar fram ett värde utifrån fönstrets höjd
        /// </summary>
        /// <returns></returns>
        private int GenerateRandomTop()
        {
            return _random.Next((int)_mainWindowHeight);
        }

        /// <summary>
        /// Randomiserar fram ett värde utifrån fönstrets bredd
        /// </summary>
        /// <returns></returns>
        private int GenerateRandomLeft()
        {
            return _random.Next((int)_mainWindowWidth);
        }

        #endregion

        #region Movement and collision
        /// <summary>
        /// Loopar genom alla ShipViewModel objekt i listan Ships för att repositionera de objekt som hamnar utanför fönstrets höjd.
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
        /// Loopar genom alla ObstacleViewModel objekt i listan Obstacles för att repositionera de objekt som hamnar utanför fönstrets höjd.
        /// </summary>
        private void MoveObstaclesLoop()
        {
            foreach (ObstacleViewModel obstacle in Obstacles)
            {
                obstacle.TopCoordinates += _speedObstacle;

                if (obstacle.TopCoordinates > _mainWindowHeight)
                {
                    obstacle.TopCoordinates = -50;
                    obstacle.LeftCoordinates = GenerateRandomLeft();
                }
            }
        }

        /// <summary>
        /// Kontrollerar och repositionerar objekt av typen ShipViewModel så att dem ej kolliderar med existerande ShipViewModel objekt ute på canvas.
        /// </summary>
        private void ResetShipPosition(ShipViewModel ship)
        {
            int newLeft = GenerateRandomLeft();

            while (IsCollisionWithExistingShips(newLeft, -50))
            {
                newLeft = GenerateRandomLeft();
            }

            ship.TopCoordinates = -50;
            ship.LeftCoordinates = newLeft;
        }

        /// <summary>
        /// Kontrollerar om objekten av typen ShipViewModel kolliderar med existerande ShipViewModel objekt ute på canvas vid en repositionering
        /// </summary>
        /// <returns></returns>
        private bool IsCollisionWithExistingShips(int left, int top) 
        {
            foreach (var existingShip in Ships) 
            {
                bool collisionXAxis = left < (existingShip.LeftCoordinates + _shipWidth + objectPaddingAtGeneration) && (left + _shipWidth + objectPaddingAtGeneration) > existingShip.LeftCoordinates;
                bool collisionYAxis = top < (existingShip.TopCoordinates + _shipHeight + objectPaddingAtGeneration) && (top + _shipHeight + objectPaddingAtGeneration) > existingShip.TopCoordinates;

                if (collisionXAxis && collisionYAxis)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Kontrollerar att objekten av typen ShipViewModel ej kolliderar med existerande ShipViewModel objekt ute på canvas vid en repositionering efter en kollision med spelaren.
        /// </summary>
        public void SetPlayerShipCollisionConsequence()
        {
            foreach (var ship in Ships)
            {
                if (PlayerCollidesWithShip(ship))
                {
                    PlayerShipCollisionReposition(ship);
                }
            }
        }

        /// <summary>
        /// Kontrollerar och repositionerar objekt av typen ShipViewModel så att dem ej kolliderar med existerande ShipViewModel objekt ute på canvas efter en kollision med spelaren.
        /// </summary>
        private void PlayerShipCollisionReposition(ShipViewModel ship)
        {
            int newLeft = GenerateRandomLeft();

            while (IsCollisionWithExistingShips(newLeft, -50))
            {
                newLeft = GenerateRandomLeft();
            }

            ship.TopCoordinates = -50;
            ship.LeftCoordinates = newLeft;
            GamePoints.AddPoints(gainedPointsFromShips);
        }

        /// <summary>
        /// Repositionerar objekten av typen ObstacleViewModel efter en kollision med spelaren.
        /// </summary>
        private void SetPlayerObstacleCollisionConsequence()
        {
            foreach (var obstacle in Obstacles)
            {
                if (PlayerCollidesWithObstacle(obstacle))
                {
                    obstacle.TopCoordinates = -50;
                    obstacle.LeftCoordinates = GenerateRandomLeft();

                    GamePoints.DeductPoints(lostPointsFromObstacles);
                    PlayerDamaged();
                    PointResult -= lostPointsFromObstacles; //TODO Någon som vet varför man måste dra av från både PointResult och köra DeductPoints?
                }
            }
        }

        /// <summary>
        /// Registrerar om spelaren kolliderar med ett objekt av typen ShipViewModel
        /// </summary>
        private bool PlayerCollidesWithShip(ShipViewModel ship)
        {
            bool collisionXAxis = ship.LeftCoordinates < (PlayerVM.LeftCoordinates + _playerWidth) && (ship.LeftCoordinates + _shipWidth) > PlayerVM.LeftCoordinates;
            bool collisionYAxis = ship.TopCoordinates < (PlayerVM.TopCoordinates + _playerHeight) && (ship.TopCoordinates + _shipHeight) > PlayerVM.TopCoordinates;

            if (collisionXAxis && collisionYAxis)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Registrerar om spelaren kolliderar med ett objekt av typen ObstacleViewModel
        /// </summary>
        public bool PlayerCollidesWithObstacle(ObstacleViewModel obstacle)
        {
            bool collisionXAxis = obstacle.LeftCoordinates < (PlayerVM.LeftCoordinates + _playerWidth) && (obstacle.LeftCoordinates + _obstacleWidth) > PlayerVM.LeftCoordinates;
            bool collisionYAxis = obstacle.TopCoordinates < (PlayerVM.TopCoordinates + _playerHeight) && (obstacle.TopCoordinates + _obstacleHeight) > PlayerVM.TopCoordinates;

            if (collisionXAxis && collisionYAxis)
            {
                return true;
            }
            return false;
        }
        #endregion

        /// <summary>
        /// Huruvida spelaren ska ta skada eller dö (game over)
        /// </summary>
        public void PlayerDamaged()
        {
            PlayerHealth -= 1;
            UpdateHearts();
            CheckIfGameOver();

        }

        /// <summary>
        /// Uppdaterar hjärtan vid kollision med Obstackle
        /// </summary>
        private void UpdateHearts()
        {
            if (PlayerHealth <= 2)
            {
                Heart3 = _emptyHeart;
            }
            if (PlayerHealth <= 1)
            {
                Heart2 = _emptyHeart;
            }
        }

        /// <summary>
        /// Om spelaren har 0 liv kvar så öppnas Game Over fönstret
        /// </summary>
        private void CheckIfGameOver()
        {
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
            StartTimerForIncreasedDifficulty();
            PlayerVM.StartPlayerTimer();
        }

        public void StopTimers()
        {
            StopGameTimer();
            StopCountdownTimer();
            StopTimerForIncreasedDifficulty();
            PlayerVM.StopPlayerTimer();
        }

        private void SetTimerForIncreasedDifficulty()
        {
            _increaseDifficultyTimer = new DispatcherTimer();
            _increaseDifficultyTimer.Interval = TimeSpan.FromSeconds(1);
        }

        private void StartTimerForIncreasedDifficulty()
        {
            _increaseDifficultyTimer.Tick += CountdownTimer_IncreaseObstacles;
            _increaseDifficultyTimer.Start();
        }
        private void StopTimerForIncreasedDifficulty()
        {
            _increaseDifficultyTimer.Tick -= CountdownTimer_IncreaseObstacles;
            _increaseDifficultyTimer.Stop();
        }

        private void SetGameTimer()
        {
            _gameTimer = new DispatcherTimer();
            _gameTimer.Interval = TimeSpan.FromMilliseconds(gameTimerUpdateFrequencyInMilliseconds);
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
            CountdownTimer = new TimerViewModel(seconds); 
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

            _backgroundMusicPlayer.Stop();
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

        #region Sound
        /// <summary>
        /// Startar musik i bakgrunden i spelvyn
        /// </summary>
        private void InitializeBackgroundMusic()
        {
            _backgroundMusicPlayer.Open(new Uri("Sounds/Background_sound.mp3", UriKind.Relative));
            _backgroundMusicPlayer.MediaEnded += (sender, e) => { _backgroundMusicPlayer.Position = TimeSpan.Zero; _backgroundMusicPlayer.Play(); };
            _backgroundMusicPlayer.Play();
        }
        #endregion

    }
}