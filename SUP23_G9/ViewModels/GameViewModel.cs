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
        private double mainWindowHeight = Application.Current.MainWindow.ActualHeight;

        public ObservableCollection<double> ShipTopCoordinates { get; }

        public GameViewModel()
        {
            int numberOfShips = 10;

            ShipTopCoordinates = new ObservableCollection<double>(GenerateRandomCoordinates(numberOfShips));

            StartMovingObject();
        }

        //Randomiserar fram objektens top position inom fönstret
        private List<double> GenerateRandomCoordinates(int numberOfShips)
        {
            Random random = new Random();

            List<double> coordinates = new List<double>();

            for (int i = 0; i < numberOfShips; i++)
            {
                double randomTop = random.Next(0, (int)mainWindowHeight);
                coordinates.Add(randomTop);
            }

            return coordinates;
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

            for (int i = 0; i < ShipTopCoordinates.Count; i++)
            {
                ShipTopCoordinates[i] += _speed;

                if (ShipTopCoordinates[i] > mainWindowHeight)
                {
                    ShipTopCoordinates[i] = 0;
                    //Randomsiera om ist för 0?
                }
            }
        }
    }
}

