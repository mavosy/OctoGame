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
        private double mainWindowHeight;
        public ObservableCollection<double> ShipTopCoordinates { get; } = new ObservableCollection<double>
        {
            100,
            200,
            300,
            400,
            500
        };


        public GameViewModel()
        {
            StartMovingObject();
        }

        private void StartMovingObject()
        {
            gameTimer = new Timer(20);
            gameTimer.Elapsed += GameTimerEvent;
            gameTimer.Start();
            mainWindowHeight = 600; //krashar när använder widnow current height, har bara satt en statisk höjd
        }

        private void GameTimerEvent(object sender, ElapsedEventArgs e)
        {
            MoveObjectDown();
        }

        private void MoveObjectDown()
        {
            //Måste iterera runt alla ships objekten för att se vilken som hamnar utanför
            for (int i = 0; i < ShipTopCoordinates.Count; i++)
            {
                double newTop = ShipTopCoordinates[i] + _speed;

                if (newTop > mainWindowHeight)
                {
                    newTop = 0;
                }

                ShipTopCoordinates[i] = newTop;
            }
        }
    }
}
    
    
