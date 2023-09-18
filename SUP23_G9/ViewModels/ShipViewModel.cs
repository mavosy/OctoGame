using SUP23_G9.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace SUP23_G9.ViewModels
{
    public class ShipViewModel : BaseViewModel
    {
        public ShipViewModel()
        {
            Width = 50;
            Height = 50;

            ShipVisibility = Visibility.Visible;

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
        public Visibility ShipVisibility { get; set; }

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