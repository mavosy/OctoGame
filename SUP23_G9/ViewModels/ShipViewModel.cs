using SUP23_G9.ViewModels.Base;
using System;
using System.Windows.Media.Imaging;

namespace SUP23_G9.ViewModels
{
    public class ShipViewModel : BaseViewModel
    {
        private bool _isAnimating = true; 

        public ShipViewModel()
        {
            Width = 50;
            Height = 50;
            LoadShipImageProcessing();
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
    }
}
