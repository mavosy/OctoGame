using SUP23_G9.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        DispatcherTimer _obstacleTimer;
        int _danceMoveCount;
        public ObstacleViewModel()
        {
            Width = 50;
            Height = 50;
            FlipImageX = 1.0;
            _obstacleTimer = new DispatcherTimer();
            _obstacleTimer.Interval = TimeSpan.FromSeconds(1);
            _obstacleTimer.Tick += ObstacleDanceEvent;
            _obstacleTimer.Start();
            LoadObstacleImageProcessing();
        }

        private void ObstacleDanceEvent(object? sender, EventArgs e)
        {
            if (FlipImageX == 1.0)
            {
                FlipImageX = -1.0;
            }
            else if (FlipImageX == -1.0)
            {
                FlipImageX = 1.0;
            }
        }

        /// <summary>
        /// X-koordinat för obstacle
        /// </summary>
        public double Left { get; set; }
        /// <summary>
        /// Y-koordinat för obstacle
        /// </summary>
        public double Top { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public BitmapImage ObstacleImage { get; set; }
        public double FlipImageX { get; private set; }

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