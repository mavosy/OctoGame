using SUP23_G9.Commands;
using SUP23_G9.ViewModels.Base;
using SUP23_G9.Views.Characters;
using SUP23_G9.Views.Components;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace SUP23_G9.ViewModels
{

    public class PlayerViewModel : BaseViewModel
    {
        readonly double _distanceToEdge = 5;
        readonly int _playerSpeed = 5;

        bool _leftButtonIsDown, _rightButtonIsDown, _upButtonIsDown, _downButtonIsDown;
        DispatcherTimer _timer;

        public PlayerViewModel()
        {
            LeftCoordinates = 500;
            TopCoordinates = 350;
            Width = 50;
            Height = 50;

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(10l);    //sätter ett intervall i millisekunder för hur ofta MovePlayerEvent ska köras
            _timer.Tick += MovePlayerEvent;     //kör MovePlayerEvent varje gång interval ska börja om
            _timer.Start();    //startar timer

            //LoadKrakenImageProcessing();
            UpKeyDownCommand = new RelayCommand(x => MovePlayerUp(), x => IsNotAtTopEdge());
            DownKeyDownCommand = new RelayCommand(x => MovePlayerDown(), x => IsNotAtBottomEdge());
            LeftKeyDownCommand = new RelayCommand(x => MovePlayerLeft(), x => IsNotAtLeftEdge());
            RightKeyDownCommand = new RelayCommand(x => MovePlayerRight(), x => IsNotAtRightEdge());
        }


        public double LeftCoordinates { get; set; }
        public double TopCoordinates { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public ICommand UpKeyDownCommand { get; private set; }
        public ICommand DownKeyDownCommand { get; private set; }
        public ICommand LeftKeyDownCommand { get; private set; }
        public ICommand RightKeyDownCommand { get; private set; }

        //public BitmapImage KrakenImage { get; set; }
        //public ScaleTransform FlipXTransform { get; set; } = new ScaleTransform();

        private void MovePlayerEvent(object? sender, EventArgs e)
        {
            if (_leftButtonIsDown && IsNotAtLeftEdge())
            {
                MovePlayerLeft(); //sätter ny x-koordinat till den förra koordinaten MINUS _playerSpeed
                        //koden gör att Kraken vänder på sig beroende på vilket håll den rör sig åt (vänster eller höger)
            }

            if (_rightButtonIsDown && IsNotAtRightEdge())
            {
                MovePlayerRight();   //sätter ny x-koordinat till den förra koordinaten PLUS _playerSpeed
                      //koden gör att Kraken vänder på sig beroende på vilket håll den rör sig åt (vänster eller höger)
            }

            if (_upButtonIsDown && IsNotAtTopEdge())
            {
                MovePlayerUp();       //sätter ny y-koordinat till den förra koordinaten MINUS _playerSpeed
            }

            if (_downButtonIsDown && IsNotAtBottomEdge())
            {
                MovePlayerDown();      //sätter ny x-koordinat till den förra koordinaten PLUS _playerSpeed
            }
        }

        public void MovePlayerLeft()
        {
            LeftCoordinates -= _playerSpeed;
        }

        public void MovePlayerRight()
        {
            LeftCoordinates += _playerSpeed;
        }

        public void MovePlayerUp()
        {
            TopCoordinates -= _playerSpeed;
        }

        public void MovePlayerDown()
        {
            TopCoordinates += _playerSpeed;
        }

        public bool IsNotAtLeftEdge()
        {
            return LeftCoordinates > _distanceToEdge; 
        }

        public bool IsNotAtRightEdge()
        {
            return (LeftCoordinates + Width) < (GlobalStatic._gameAreaRenderedWidth - _distanceToEdge);
        }

        public bool IsNotAtTopEdge()
        {
            return TopCoordinates > _distanceToEdge;
        }

        public bool IsNotAtBottomEdge()
        {
            return (TopCoordinates + Height) < (GlobalStatic._gameAreaRenderedHeight - _distanceToEdge);
        }

        internal void HandleKeyDown(KeyEventArgs e)
        {
            switch (e.Key)   //I KeyEventArgs finns värdet "Key" som helt enkelt är vilken knapp som har tryckts ner, typ P, R, 8, Enter, NumLock etc. Switchar på det och sätter bool till true
            {
                case Key.J:        //här kan man ange typ vilka tangenter som helst, det ändrar kontrollen till spelet. Kanske ha userInput så man kan ändra själv?
                    _leftButtonIsDown = true;
                    break;

                case Key.L:
                    _rightButtonIsDown = true;
                    break;

                case Key.I:
                    _upButtonIsDown = true;
                    break;

                case Key.K:
                    _downButtonIsDown = true;
                    break;

                default:
                    break;
            }
        }

        internal void HandleKeyUp(KeyEventArgs e)
        {
            switch (e.Key)   //I KeyEventArgs finns värdet "Key" som helt enkelt är vilken knapp som har tryckts ner, typ P, R, 8, Enter, NumLock etc. Switchar på det och sätter bool till true
            {
                case Key.J:        //här kan man ange typ vilka tangenter som helst, det ändrar kontrollen till spelet. Kanske ha userInput så man kan ändra själv?
                    _leftButtonIsDown = false;
                    break;

                case Key.L:
                    _rightButtonIsDown = false;
                    break;

                case Key.I:
                    _upButtonIsDown = false;
                    break;

                case Key.K:
                    _downButtonIsDown = false;
                    break;

                default:
                    break;
            }
        }

        //private void LoadKrakenImageProcessing()
        //{
        //    BitmapImage image = new BitmapImage();
        //    image.BeginInit();
        //    image.UriSource = new Uri("C:\\Users\\OWNER\\source\\repos\\SUP23_G9\\SUP23_G9\\Views\\Components\\Images\\Happy_Kraken_Left.bmp", UriKind.Relative);
        //    image.DecodePixelWidth = 50;
        //    image.CacheOption = BitmapCacheOption.OnLoad;
        //    image.EndInit();

        //    KrakenImage = image;
        //}


        //private void TurnSpriteHorizontally()
        //{
        //    FlipXTransform.ScaleX = -1.0;
        //}        
        //private void TurnSpriteBack()
        //{
        //    FlipXTransform.ScaleX = 1.0;

        //}

        //private void TurnSpriteHorizontally()
        //{
            
        //    _flipHorizontally;
        //}

        //private void ResetImage()
        //{
        //    imageControl.RenderTransform = null;
        //}

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
        //    image.Source = new BitmapImage(new Uri(@newImageURI, UriKind.Relative)));   //byter imagesource till ny källa
        //}
    }
}
