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
using System.Windows.Threading;
using static System.Formats.Asn1.AsnWriter;

namespace SUP23_G9.ViewModels
{
    public class GameViewModel : BaseViewModel
    {
        private DispatcherTimer _gameTimer;
        private readonly int _speed = 4;
        private readonly int _speedObstacle = 2;
        private static readonly Random _random = new();
        public int PointResult { get; set; }
        private bool isGameActive = true;   // Nytt boolskt fält som indikerar om spelet är aktivt eller inte.
        public PlayerViewModel PlayerViewModel { get; private set; }// en ny instans av PlayerViewModel inom din GameViewModel
        private double _mainWindowHeight = Application.Current.MainWindow.ActualHeight;
        private double _mainWindowWidth = Application.Current.MainWindow.ActualWidth;
        public TimerViewModel CountdownTimer { get; set; } = new TimerViewModel(45); // Startar med 1 min.
        

        public GameViewModel()
        {
            //this.PlayerViewModel = new PlayerViewModel();
            Ships = new ObservableCollection<ShipViewModel>();
            Obstacles = new ObservableCollection<ObstacleViewModel>();
            PlayerHealth = 100;
            CreateRandomShips();
            CreateRandomObstacles();
            StartMovingObject();
            CountdownTimer.TimeUp += CountdownTimer_TimeUp;
        }

        public Points GamePoints { get; } = new Points();

        public ObservableCollection<ShipViewModel> Ships { get; set; }
        public ObservableCollection<ObstacleViewModel> Obstacles { get; set; }
        /// <summary>
        /// Spelarens livspoäng, är bundet till ProgressBar i UI
        /// </summary>
        public int PlayerHealth { get; set; }

        private void CreateRandomShips()
        {
            for (int i = 0; i < 10; i++) // Skapa 10 skepp
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

        private void CreateRandomObstacles()
        {
            for (int i = 0; i < 5; i++) //Obstacles 2st
            {
                int randomTop = GenerateRandomTop() - 600;
                int randomLeft = GenerateRandomLeft();
                Obstacles.Add(new ObstacleViewModel { Top = randomTop, Left = randomLeft });
            }
        }

        private int GenerateRandomTop() => _random.Next((int)_mainWindowHeight);
        private int GenerateRandomLeft() => _random.Next((int)_mainWindowWidth); 

        private void StartMovingObject()
        {
            _gameTimer = new DispatcherTimer();
            Debug.WriteLine($"Setting up GameViewModel with ID: {this.InstanceID}");
            _gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            _gameTimer.Tick += GameTimerEvent;
            _gameTimer.Start();
        }

        private void GameTimerEvent(object sender, EventArgs e)
        {
            MoveShipsLoop();
            MoveObstaclesLoop();
            SetPlayerShipCollisionConsequence();
            SetPlayerObstacleCollisionConsequence();
            Debug.WriteLine($"Moving objects with GameViewModel with ID: {InstanceID}");
        }
        /// <summary>
        /// Loopar genom skeppen för att hitta vilka som "ramlar" ut ur fönstret för att sedan repositionera till 0
        /// </summary>
        private void MoveShipsLoop()
        {
            foreach (ShipViewModel ship in Ships)
            {
                ship.Top += _speed;

                if (ship.Top > _mainWindowHeight)
                {

                    ResetShipPosition(ship);
                }
            }
        }

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

        private void SetPlayerObstacleCollisionConsequence()
        {
            if (!isGameActive) return; // Om spelet inte är aktivt, gör ingenting och avbryt metoden.
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
        /// <summary>
        /// Metod för om spelaren skadas eller dör
        /// </summary>
        public void PlayerDamaged()
        {
            PlayerHealth -= 34;
            if (PlayerHealth <= 0)
            {
                OpenGameOverView();
            }
        }
        public void SetPlayerShipCollisionConsequence()
        {
            if (!isGameActive) return; // Om spelet inte är aktivt, gör ingenting och avbryt metoden.
            foreach (var ship in Ships)
            {
                if (PlayerCollidesWithShip(ship))
                {
                    int newLeft = GenerateRandomLeft();

                    while (true)
                    {
                        bool collides = false;
                        foreach (var existingShip in Ships)
                        {
                            bool collisionX = newLeft < existingShip.Left + 55 && newLeft + 55 > existingShip.Left;
                            bool collisionY = 0 < existingShip.Top + 55 && 0 + 55 > existingShip.Top;

                            if (collisionX && collisionY)
                            {
                                collides = true;
                                break;
                            }
                        }

                        if (!collides)
                        {
                            ship.Top = 0;
                            ship.Left = newLeft;

                            GamePoints.AddPoints(10);
                            break;
                        }

                        newLeft = GenerateRandomLeft();
                    }
                }
            }
        }
        public void StartGame()
        {
            isGameActive = true; // sätt detta till true när spelet startar om.

        }

        public void StopGame()
        {
            isGameActive = false; // sätt detta till false när spelet är över.

        }
        //TODO ändra så det inte är fasta värden på width/height här (50)
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

        private void CountdownTimer_TimeUp(object sender, EventArgs e)
        {
            CountdownTimer._timer.Stop();
            // Anropa ShowGameOverView när tiden tar slut
            this.StopGame(); // Stoppa spelet.
            StopAllTimersAndObjects();
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
            // Anropa ShowGameOverView när tiden tar slut från rätt tråd
            //Application.Current.Dispatcher.Invoke(() =>
            //{
            //    // Stäng det nuvarande fönstret (MainWindow) om det behövs
            //    foreach (Window window in Application.Current.Windows)
            //    {
            //        if (window is MainWindow)
            //        {
            //            window.Close();
            //            break; // Stäng bara det första förekomsten av MainWindow
            //        }
            //    }
            //});
        }
        public event Action<int> GameOverEvent;
        public void StopAllTimersAndObjects()
        {
            // Stoppa Game Timer
            _gameTimer?.Stop();

            // Stoppa Player Timer
            //this.PlayerViewModel.StopPlayerTimer();
            // Stoppa Countdown Timer
            CountdownTimer._timer?.Stop();

        }
        public void SomeMethodWhereGameEndsOrUserExits()
        {
            this.StopAllTimersAndObjects(); // Stoppar alla timers och objekt.
                                            // Övrig kod för att hantera spelavslut eller användaravslut.
        }
        public void StopGameTimer()
        {
            _gameTimer.Stop();
        }
    }
}