using SUP23_G9.ViewModels.Base;
using System;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;

namespace SUP23_G9.ViewModels
{
    public class ObstacleViewModel : BaseViewModel
    {
        public const int width = 50;
        public const int height = 50;

        public ObstacleViewModel()
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
        public BitmapImage SpriteImage { get; set; }

        private void LoadImageProcessing()
        {
            BitmapImage image = new BitmapImage();

            image.BeginInit();
            image.UriSource = new Uri("pack://application:,,,/SUP23_G9;component/Views/Components/Images/PirateIsland1.bmp");
            image.DecodePixelWidth = width;
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.EndInit();

            SpriteImage = image;
        }
    }
}