﻿using SUP23_G9.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace SUP23_G9.ViewModels
{
    public class ObstacleViewModel : BaseViewModel
    {
        private bool _isAnimating = true;

        public ObstacleViewModel()
        {
            Width = 50;
            Height = 50;
            LoadObstacleImageProcessing();
        }

        public bool IsAnimating
        {
            get { return _isAnimating; }
            set
            {
                _isAnimating = value;
                OnPropertyChanged(nameof(IsAnimating));
            }
        }

        /// <summary>
        /// X-koordinat för hindren
        /// </summary>
        public double Left { get; set; }
        /// <summary>
        /// Y-koordinat för hindren
        /// </summary>
        public double Top { get; set; }
        /// <summary>
        /// Hindrens bredd
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// Hindrens höjd
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// Bild för hindren
        /// </summary>
        public BitmapImage ObstacleImage { get; set; }

        /// <summary>
        /// Processerar och cachar bild för hinder
        /// </summary>
        private void LoadObstacleImageProcessing()
        {
            BitmapImage image = new BitmapImage();

            image.BeginInit();
            image.UriSource = new Uri("pack://application:,,,/SUP23_G9;component/Views/Components/Images/PirateIsland1.bmp");
            image.DecodePixelWidth = 50;
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.EndInit();

            ObstacleImage = image;
        }
    }
}