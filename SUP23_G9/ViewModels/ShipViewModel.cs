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
        /// <summary>
        /// Skeppens bredd
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// Skeppens höjd
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// Bild för skeppen
        /// </summary>
        public BitmapImage ShipImage { get; set; }
        /// <summary>
        /// Sätter vilket håll skeppen är vänd mot i horisontellt led (1.0=original, -1.0=spegel)
        /// </summary>
        public double FlipImageX { get; set; } 

        /// <summary>
        /// Processerar och cachar bild för skepp
        /// </summary>
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
        /// <summary>
        /// Får skeppen att vända på sig med jämna intervaller
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
    }
}