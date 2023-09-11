using SUP23_G9.ViewModels.Base;
using SUP23_G9.Views.Characters;
using SUP23_G9.Views.Components;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace SUP23_G9.ViewModels
{
    class GameViewModel : BaseViewModel
    {
        public ObservableCollection<GameGrid>? Ocean { get; private set; }

        private const int _gameGridSize = 10;

        public GameViewModel()
        {
            FillGrid();
        }

        private void FillGrid()
        {
            Ocean = new ObservableCollection<GameGrid>();
            for (int x = 0; x < _gameGridSize; x++)
            {
                for (int y = 0; y < _gameGridSize; y++)
                {
                    var piece = new GameGrid()
                    {
                        X = x,
                        Y = y,
                    };
                    Ocean.Add(piece);

                }

            }

        }

        /// <summary>
        /// Sätter horizontell rörelse och hastighet
        /// </summary>
        /// <param name="imageComponent"></param>
        /// <param name="speed"></param>
        private void SetLeftMovement()
        {
            Ship ship = new Ship();
            double speed = 10;
            Canvas.SetLeft(ship, Canvas.GetLeft(ship) - speed);
        }

        /// <summary>
        /// Sätter vertikal rörelse och hastighet
        /// </summary>
        /// <param name="imageComponent"></param>
        /// <param name="speed"></param>
        private void SetTopMovement(Image image, int speed)
        {
            Canvas.SetTop(image, Canvas.GetTop(image) - speed);
        }
    }
}