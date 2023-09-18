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
    public class ShipViewModel : BaseViewModel
    {
        DispatcherTimer _shipTimer;
        public ShipViewModel()
        {
            Width = 50;
            Height = 50;
            FlipImageX = 1.0;

            _shipTimer = new DispatcherTimer();
            _shipTimer.Interval = TimeSpan.FromSeconds(2);
            _shipTimer.Tick += ObstacleDanceEvent;
            _shipTimer.Start();

            LoadShipImageProcessing();
        }
        /// <summary>
        /// X-koordinat för skeppen
        /// </summary>
        public double Left { get; set; }
        /// <summary>
        /// Y-koordinat för skeppen
        /// </summary>
        public double Top { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public BitmapImage ShipImage { get; set; }
        public double FlipImageX { get; set; }

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
        private void LoadShipImageProcessing()
        {
            BitmapImage image = new BitmapImage();

            image.BeginInit();
            image.UriSource = new Uri("pack://application:,,,/SUP23_G9;component/Views/Components/Images/PirateShip1_Right.bmp");
            image.DecodePixelWidth = 50;
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.EndInit();

            ShipImage = image;
        }
    }
}