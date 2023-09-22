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

        private double _mainWindowHeight = Application.Current.MainWindow.ActualHeight;
        private double _mainWindowWidth = Application.Current.MainWindow.ActualWidth;
        public TimerViewModel CountdownTimer { get; set; } = new TimerViewModel(30); // Startar med 1 min.
        

        public GameViewModel()
        {
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
            for (int i = 0; i < 10; i++) //Ships 2st
            {
                int randomTop = GenerateRandomTop();
                int randomLeft = GenerateRandomLeft();
                Ships.Add(new ShipViewModel { Top = randomTop, Left = randomLeft });
            }
        }

        private void CreateRandomObstacles()
        {
            for (int i = 0; i < 2; i++) //Obstacles 2st
            {
                int randomTop = GenerateRandomTop();
                int randomLeft = GenerateRandomLeft();
                Obstacles.Add(new ObstacleViewModel { Top = randomTop, Left = randomLeft });
            }
        }

        private int GenerateRandomTop() => _random.Next((int)_mainWindowHeight);
        private int GenerateRandomLeft() => _random.Next((int)_mainWindowWidth); 

        private void StartMovingObject()
        {
            _gameTimer = new DispatcherTimer();
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
                    ship.Top = 0;
                    ship.Left = GenerateRandomLeft();
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

        private void SetPlayerObstacleCollisionConsequence()
        {
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
            foreach (var ship in Ships)
            {
                if (PlayerCollidesWithShip(ship))
                {
                    ship.Top = 0;
                    ship.Left = GenerateRandomLeft();

                    GamePoints.AddPoints(10);
                    PointResult += 10;

                }
            }
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
        
        public void StopGameTimer()
        {
            _gameTimer.Stop();
        }
    }
}