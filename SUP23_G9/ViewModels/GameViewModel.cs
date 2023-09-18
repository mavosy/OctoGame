﻿using SUP23_G9.ViewModels.Base;
using SUP23_G9.Views.Characters;
using SUP23_G9.Views.Components;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private readonly int _speed = 4;
        private readonly int _speedObstacle = 2;
        private static readonly Random _random = new();

        private double _mainWindowHeight = Application.Current.MainWindow.ActualHeight;
        private double _mainWindowWidth = Application.Current.MainWindow.ActualWidth;
        public TimerViewModel CountdownTimer { get; set; } = new TimerViewModel(60); // Startar med 1 min.

        private Points _pointsSystem = new Points(); // Poäng

        public int Score
        {
            get => _pointsSystem.Score;
        }
        public GameViewModel()
        {
            Ships = new ObservableCollection<ShipViewModel>();
            Obstacles = new ObservableCollection<ObstacleViewModel>();
            CreateRandomShips();
            StartMovingObject();
        }

        public ObservableCollection<ShipViewModel> Ships { get; set; }
        public ObservableCollection<ObstacleViewModel> Obstacles { get; set; }

        private void CreateRandomShips()
        {
            for (int i = 0; i < 10; i++) //Ships 2st
            {
                int randomTop = GenerateRandomTop();
                int randomLeft = GenerateRandomLeft();
                Ships.Add(new ShipViewModel { Top = randomTop, Left = randomLeft });
            }

            for (int i = 0; i < 5; i++) //Obstacles 2st
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
            _gameTimer = new Timer(20);
            _gameTimer.Elapsed += GameTimerEvent;
            _gameTimer.Start();
        }

        private void GameTimerEvent(object sender, ElapsedEventArgs e)
        {
            MoveObjectsLoop();
            CollisionCheck();
        }

        // Loopar genom objekten för att hitta vilka som "ramlar" ut ur fönstret för att sedan repositionera till 0
        private void MoveObjectsLoop()
        {
            foreach (var ship in Ships)
            {
                ship.Top += _speed;

                if (ship.Top > _mainWindowHeight)
                {
                    ship.Top = 0;
                    ship.Left = GenerateRandomLeft();
                }
            }

            foreach (var obstacle in Obstacles)
            {
                obstacle.Top += _speedObstacle;

                if (obstacle.Top > _mainWindowHeight)
                {
                    obstacle.Top = 0;
                    obstacle.Left = GenerateRandomLeft();
                }
            }
        }

        //Version 2.0 på metoden CollisionCheck() där bläckfisken istället äter upp alla skeppen = skeppen försvinner helt från canvas
        private void CollisionCheck()
        {
            List<ShipViewModel> shipsRemove = new List<ShipViewModel>();

            foreach (var ship in Ships)
            {
                bool collisionX = ship.Left < GlobalVariabels._playerCoordinatesLeft + 50 && ship.Left + 50 > GlobalVariabels._playerCoordinatesLeft;
                bool collisionY = ship.Top < GlobalVariabels._playerCoordinatesTop + 50 && ship.Top + 50 > GlobalVariabels._playerCoordinatesTop;

                if (collisionX && collisionY)
                {
                    //MessageBox.Show("nom nom nom");
                    shipsRemove.Add(ship);
                }
            }

            //https://stackoverflow.com/questions/18331723/this-type-of-collectionview-does-not-support-changes-to-its-sourcecollection-fro
            foreach (var shipRemove in shipsRemove)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Ships.Remove(shipRemove);
                });
            }

        }

    }
}