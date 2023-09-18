using SUP23_G9.ViewModels.Base;
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
        private static readonly Random _random = new();

        private double _mainWindowHeight = Application.Current.MainWindow.ActualHeight;
        private double _mainWindowWidth = Application.Current.MainWindow.ActualWidth;
        public GameViewModel()
        {
            Ships = new ObservableCollection<ShipViewModel>();
            CreateRandomShips();
            StartMovingObject();
        }

        public ObservableCollection<ShipViewModel> Ships { get; set; }

        private void CreateRandomShips()
        {
            for (int i = 0; i < 10; i++)
            {
                int randomTop = GenerateRandomTop();
                int randomLeft = GenerateRandomLeft();
                Ships.Add(new ShipViewModel { Top = randomTop, Left = randomLeft });
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
        }

        private void CollisionCheck() //laggar otroligt mycket.
        {
            PlayerViewModel playerViewModel = new PlayerViewModel();

            foreach (var ship in Ships)
            {
                bool collisionX = ship.Left < playerViewModel.LeftCoordinates + playerViewModel.Width && ship.Left + ship.Width > playerViewModel.LeftCoordinates;
                bool collisionY = ship.Top < playerViewModel.TopCoordinates + playerViewModel.Height && ship.Top + ship.Width > playerViewModel.TopCoordinates;

                if (collisionX && collisionY)
                {
                    //MessageBox.Show("nom nom nom");
                    //ship.ShipImage = null;
                }
            }
        }
    }
}