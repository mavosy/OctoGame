using SUP23_G9.ViewModels.Base;
using System;
using System.Windows.Media.Imaging;

namespace SUP23_G9.ViewModels
{
    public class ShipViewModel : BaseViewModel
    {
        public const int width = 50;
        public const int height = 50;

        public ShipViewModel()
        {
            Width = width;
            Height = height;
            IsAnimating = true;
            LoadImageProcessing();
        }

        public double LeftCoordinates { get; set; }
        public double TopCoordinates { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool IsAnimating { get; set; }
        public BitmapImage SpriteImage { get; private set; }

        private void LoadImageProcessing()
        {
            BitmapImage image = new BitmapImage();

            image.BeginInit();
            image.UriSource = new Uri("pack://application:,,,/SUP23_G9;component/Views/Components/Images/PirateShip1_Right.bmp");
            image.DecodePixelWidth = width;
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.EndInit();

            SpriteImage = image;
        }
    }
}
