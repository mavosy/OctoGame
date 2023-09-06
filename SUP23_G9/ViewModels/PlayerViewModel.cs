﻿using SUP23_G9.Commands;
using SUP23_G9.ViewModels.Base;
using SUP23_G9.Views.Components;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;

namespace SUP23_G9.ViewModels
{
    internal abstract class PlayerViewModel : BaseViewModel
    {
        /// <summary>
        /// Gamegrid
        /// </summary>
        public ObservableCollection<GameGrid>? Ocean { get; private set; }

        private const int _gameGridSize = 10;

        public PlayerViewModel()
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


    }
}