using SUP23_G9.ViewModels.Base;
using System;
using System.Windows.Media.Imaging;

namespace SUP23_G9.ViewModels
{
    public class ObstacleViewModel : BaseViewModel
    {
        public ObstacleViewModel()
        {
            Width = 50;
            Height = 50;
            LoadObstacleImageProcessing();
        }

        public bool IsAnimating { get; set; } = true;


        public double Left { get; set; }
        public double Top { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public BitmapImage ObstacleImage { get; set; }
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