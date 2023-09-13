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
        //public ObservableCollection<GameGrid>? Ocean { get; private set; }

        //public double LeftCoordinates { get; set; }
        // public double TopCoordinates { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        //private const int _gameGridSize = 10;

        private double mainWindowHeight = Application.Current.MainWindow.ActualHeight;

        public ObservableCollection<double> ShipTopCoordinates { get; } = new ObservableCollection<double>
        {
            100,
            200,
            300,
            400,
            500,
            600
        };


        public GameViewModel()
        {

            //LeftCoordinates = 500;
            Width = 50;
            Height = 50;

            StartMovingObject();
            //FillGrid();
        }

        private void StartMovingObject()
        {
            gameTimer = new Timer(20);
            gameTimer.Elapsed += GameTimerEvent;
            gameTimer.Start();
        }

        private void GameTimerEvent(object sender, ElapsedEventArgs e)
        {
            MoveObjectDown();
        }

        private void MoveObjectDown()
        {

                for (int i = 0; i < ShipTopCoordinates.Count; i++)
                {
                    ShipTopCoordinates[i] += _speed;
        //private void FillGrid()
        //{
        //    Ocean = new ObservableCollection<GameGrid>();
        //    for (int x = 0; x < _gameGridSize; x++)
        //    {
        //        for (int y = 0; y < _gameGridSize; y++)
        //        {
        //            var piece = new GameGrid()
        //            {
        //                X = x,
        //                Y = y,
        //            };

                    if (ShipTopCoordinates[i] > mainWindowHeight)
                    {
                        ShipTopCoordinates[i] = 0;
                    }
                }
            }
        }
    }
        //            Ocean.Add(piece);
        //        }
        //    }
        //}
    }
}