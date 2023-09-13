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
    class GameViewModel : BaseViewModel
    {
        private Timer gameTimer;
        private readonly int _speed = 4;

        //public double LeftCoordinates { get; set; }
        // public double TopCoordinates { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

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

                    if (ShipTopCoordinates[i] > mainWindowHeight)
                    {
                        ShipTopCoordinates[i] = 0;
                    }
                }
            }
        }
    }
