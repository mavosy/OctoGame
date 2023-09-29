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
        /// <summary>
        /// X-koordinater för objekt av typen ObstacleViewModel
        /// </summary>
        public double LeftCoordinates { get; set; }
        /// <summary>
        /// Y-koordinater för objekt av typen ObstacleViewModel
        /// </summary>
        public double TopCoordinates { get; set; }
        /// <summary>
        /// Breddmått för objekt av typen ObstacleViewModel, värde hämtas från konstanten "width".
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// Höjdmått för objekt av typen ObstacleViewModel, värde hämtas från konstanten "height".
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// Används för att bestämma om objekt av typen ObstacleViewModel ska animeras eller inte
        /// </summary>
        public bool IsAnimating { get; set; }
        /// <summary>
        /// Bild som representerar objekt av typen ObstacleViewModel i gränssnittet
        /// </summary>
        public BitmapImage SpriteImage { get; set; }
        /// <summary>
        /// Processerar bild till lämpligt format för bild tillhörande objekt av typen ObstacleViewModel, samt tilldelar bild till egenskapen SpriteImage
        /// </summary>
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