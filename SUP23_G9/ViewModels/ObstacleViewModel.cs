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

        private TimerViewModel timerViewModel = new TimerViewModel();

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

            timerViewModel.StopObstacleTimerEvent += () => StopObstacleTimer();
        }

        private void ObstacleDanceEvent(object? sender, EventArgs e)
        {
            _obstacleTimer.IsEnabled=false;
            if (FlipImageX == 1.0)
            {
                FlipImageX = -1.0;
            }
            else if (FlipImageX == -1.0)
            {
                FlipImageX = 1.0;
            }
            _obstacleTimer.IsEnabled = true;
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
        /// Sätter vilket håll hindren är vänd mot i horisontellt led (1.0=original, -1.0=spegel)
        /// </summary>
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
        public void StopObstacleTimer()
        {
            _obstacleTimer.Stop();
        }
    }
}