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

namespace SUP23_G9.ViewModels
{
    public class GameViewModel : BaseViewModel
    {
        private Timer _gameTimer;
        private readonly int _speed = 3;
        private readonly int _speedObstacle = 2;
        private static readonly Random _random = new();

        private TimerViewModel timerViewModel = new TimerViewModel();

        private double _mainWindowHeight = Application.Current.MainWindow.ActualHeight;
        private double _mainWindowWidth = Application.Current.MainWindow.ActualWidth;
        public TimerViewModel CountdownTimer { get; set; } = new TimerViewModel(10); // Startar med 1 min. 

        public GameViewModel()
        {
            Ships = new ObservableCollection<ShipViewModel>();
            Obstacles = new ObservableCollection<ObstacleViewModel>();
            PlayerHealth = 100;
            CreateRandomShips();
            CreateRandomObstacles();
            StartMovingObject();
            CountdownTimer.TimeUp += CountdownTimer_TimeUp;

            timerViewModel.StopGameTimerEvent += () => StopGameTimer();
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
            for (int i = 0; i < 20; i++) // Skapa 10 skepp
            {
                int randomTop;
                int randomLeft;
                ShipViewModel newShip;

                do
                {
                    randomTop = GenerateRandomTop();
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
            for (int i = 0; i < 6; i++) //Obstacles 2st
            {
                int randomTop = GenerateRandomTop();
                int randomLeft = GenerateRandomLeft();
                Obstacles.Add(new ObstacleViewModel { Top = randomTop, Left = randomLeft });
            }
        }

        private int GenerateRandomTop() => _random.Next((int)_mainWindowHeight - 50);
        private int GenerateRandomLeft() => _random.Next((int)_mainWindowWidth - 50); 

        private void StartMovingObject()
        {
            _gameTimer = new Timer(20);
            _gameTimer.Elapsed += GameTimerEvent;
            _gameTimer.Start();
        }

        private void GameTimerEvent(object sender, ElapsedEventArgs e)
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

                    ResetShipPosition(ship);
                }
            }
        }

        private void ResetShipPosition(ShipViewModel ship)
        {
            int newLeft = GenerateRandomLeft();

            while(IsCollisionWithExistingShips(newLeft, -10))
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

        //Den gamla koden

        //    if (ship.Top > _mainWindowHeight)
        //    {
        //        ship.Top = 0;
        //        ship.Left = GenerateRandomLeft();
        //    }
        //}


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
            //switch (PlayerHealth)
            //{
            //    case > 0:
            //        PlayerHealth -= 34;
            //        break;
            //    case <= 0:
            //        OpenGameOverView();
            //        break;
            //}

            PlayerHealth -= 25; //Jämnt tal annars reagerar ej, 100 - 25 - 25 -25 -25 blir noll ej 34

            if (PlayerHealth <= 0)
            {
                OpenGameOverView();
            }
        }
        public void SetPlayerShipCollisionConsequence()
        {
            //foreach (var ship in Ships)
            //{
            //    if (PlayerCollidesWithShip(ship))
            //    {
            //        ship.Top = 0;
            //        ship.Left = GenerateRandomLeft();

            //        GamePoints.AddPoints(10);
            //    }
            //}

            //Dela upp denna också?

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
            // Anropa ShowGameOverView när tiden tar slut
            OpenGameOverView();
        }

        public event Action SwitchToGameOverViewEvent;
        public void RaiseSwitchToGameOverViewEvent() => SwitchToGameOverViewEvent?.Invoke();

        public void OpenGameOverView()
        {
            RaiseSwitchToGameOverViewEvent();
            // Anropa ShowGameOverView när tiden tar slut från rätt tråd
            //Application.Current.Dispatcher.Invoke(() =>
            //{
            //    if (isGameOverViewOpened) return;
            //    _gameOverView = new GameOverView();
            //    _gameOverView.ContentRendered += delegate { isGameOverViewOpened = true; };
            //    _gameOverView.Closed += delegate { isGameOverViewOpened = false; };
            //    _gameOverView.Show();

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
        public void StopGameTimer()
        {
            _gameTimer.Stop();
        }
    }
}