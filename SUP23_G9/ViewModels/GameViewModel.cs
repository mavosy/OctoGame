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
        private Timer gameTimer;
        private readonly int _speed = 4;
        private static readonly Random _random = new();

        private double mainWindowHeight = Application.Current.MainWindow.ActualHeight;
        private double mainWindowWidth = Application.Current.MainWindow.ActualWidth;

        public ObservableCollection<ShipViewModel> Ships { get; set; }

        public GameViewModel()
        {
            Ships = new ObservableCollection<ShipViewModel>();
            CreateRandomShips();
            StartMovingObject();
        }

        private void CreateRandomShips()
        {
            for (int i = 0; i < 10; i++)
            {
                int randomTop = GenerateRandomTop();
                int randomLeft = GenerateRandomLeft();
                Ships.Add(new ShipViewModel { Top = randomTop, Left = randomLeft });
            }
        }

        private int GenerateRandomTop()
        {
            int maxHeight = (int)mainWindowHeight;
            return _random.Next(maxHeight);
        }

        private int GenerateRandomLeft()
        {
            int maxWidth = (int)mainWindowWidth;
            return _random.Next(maxWidth);
        }

        private void StartMovingObject()
        {
            gameTimer = new Timer(20);
            gameTimer.Elapsed += GameTimerEvent;
            gameTimer.Start();
        }

        private void GameTimerEvent(object sender, ElapsedEventArgs e)
        {
            MoveObjectsLoop();
        }

        // Loopar genom objekten för att hitta vilka som "ramlar" ut ur fönstret för att sedan repositionera till 0
        private void MoveObjectsLoop()
        {

            foreach (var ship in Ships)
            {
                ship.Top += _speed;

                if (ship.Top > mainWindowHeight)
                {

                    ship.Top = 0;
                    ship.Left = GenerateRandomLeft();
                }
            }
        }
    }
}