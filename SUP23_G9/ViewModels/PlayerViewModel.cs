using SUP23_G9.Commands;
using SUP23_G9.ViewModels.Base;
using SUP23_G9.Views.Characters;
using SUP23_G9.Views.Components;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace SUP23_G9.ViewModels
{

    public class PlayerViewModel : BaseViewModel
    {
        readonly double _distanceToEdge = 5;
        readonly int _playerSpeed = 10;

        public PlayerViewModel()
        {
            LeftCoordinates = 500;
            TopCoordinates = 350;
            Width = 50;
            Height = 50;
            UpKeyDownCommand = new RelayCommand(x => MovePlayerUp(), x=> IsNotAtTopEdge());
            DownKeyDownCommand = new RelayCommand(x => MovePlayerDown(), x=> IsNotAtBottomEdge());
            LeftKeyDownCommand = new RelayCommand(x => MovePlayerLeft(), x=> IsNotAtLeftEdge());
            RightKeyDownCommand = new RelayCommand(x => MovePlayerRight(), x=> IsNotAtRightEdge());
        }

        public double LeftCoordinates { get; set; }
        public double TopCoordinates { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public ICommand UpKeyDownCommand { get; private set; }
        public ICommand DownKeyDownCommand { get; private set; }
        public ICommand LeftKeyDownCommand { get; private set; }
        public ICommand RightKeyDownCommand { get; private set; }

        /// <summary>
        /// Sätter horizontell rörelse och hastighet
        /// </summary>
        /// <param name="imageComponent"></param>
        /// <param name="speed"></param>
        private void MovePlayerLeft()
        {
            LeftCoordinates -= _playerSpeed;
        }
        
        private void MovePlayerRight()
        {
            LeftCoordinates += _playerSpeed;
        }

        /// <summary>
        /// Sätter vertikal rörelse och hastighet
        /// </summary>
        /// <param name="imageComponent"></param>
        /// <param name="speed"></param>
        private void MovePlayerUp()
        {
            TopCoordinates -= _playerSpeed;
        }
        
        private void MovePlayerDown()
        {
            TopCoordinates += _playerSpeed;
        }

        /// <summary>
        /// Kollar om en 'image' är tillräckligt långt från kanten eller inte.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="distanceToEdge"></param>
        /// <returns></returns>
        private bool IsNotAtLeftEdge()
        {
            return LeftCoordinates > _distanceToEdge; 
        }

        /// <summary>
        /// Kollar om en 'image' är tillräckligt långt från kanten eller inte.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="distanceToEdge"></param>
        /// <returns></returns>
        private bool IsNotAtRightEdge()
        {
            return LeftCoordinates + Width < Application.Current.MainWindow.ActualWidth - 70 - _distanceToEdge;
        }

        /// <summary>
        /// Kollar om en 'image' är tillräckligt långt från kanten eller inte.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="distanceToEdge"></param>
        /// <returns></returns>
        private bool IsNotAtTopEdge()
        {
            return TopCoordinates > _distanceToEdge;
        }

        /// <summary>
        /// Kollar om en 'image' är tillräckligt långt från kanten eller inte.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="distanceToEdge"></param>
        /// <returns></returns>
        private bool IsNotAtBottomEdge()
        {
            return TopCoordinates + Height < Application.Current.MainWindow.ActualHeight - 93 - _distanceToEdge;
        }

        //private void CollisionCheck(Image image1, Image image2)    //Funkar men inte helt felfritt
        //{
        //    bool collisionX = Dispatcher.Invoke(() => Canvas.GetLeft(image1) < Canvas.GetLeft(image2) + (image2.Width)) && Dispatcher.Invoke(() => Canvas.GetLeft(image1) + (image1.Width) > Canvas.GetLeft(image2));
        //    bool collisionY = Dispatcher.Invoke(() => Canvas.GetTop(image1) < Canvas.GetTop(image2) + (image2.Height)) && Dispatcher.Invoke(() => Canvas.GetTop(image1) + (image1.Height) > Canvas.GetTop(image2));

        //    if (collisionX && collisionY)
        //    {
        //        //_timer.Stop();
        //        //_timer.Enabled = false;
        //        Dispatcher.Invoke(() => image2.Source = null);
        //        //MessageBox.Show("The pirate ship was dragged down to the dark depths of the ocean, crushed by slimy tendrils and eaten for dinner");
        //    }
        //}
        ///// <summary>
        ///// Byter en bildkälla i UI till en ny
        ///// </summary>
        ///// <param name="image"></param>
        ///// <param name="newImageURI"></param>
        //private void ChangeImage(Image image, string newImageURI)
        //{
        //    Dispatcher.Invoke(() => image.Source = new BitmapImage(new Uri(@newImageURI, UriKind.Relative)));   //byter imagesource till ny källa
        //}
    }
}
