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

        public double Left { get; set; }

        public double Top { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public BitmapImage ShipImage { get; private set; }

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
