﻿using SUP23_G9.ViewModels.Base;
using SUP23_G9.Views.Characters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using static System.Net.Mime.MediaTypeNames;
using Application = System.Windows.Application;
using Image = System.Windows.Controls.Image;

namespace SUP23_G9.Views
{
    /// <summary>
    /// Interaction logic for GameView.xaml
    /// </summary>
    public partial class GameView : UserControl, INotifyPropertyChanged
    {
        #region Field-variables
        Timer? _timer = new();     //nyar upp en ny timer från namespace System.Timers
        
        bool _leftButtonIsDown, _rightButtonIsDown, _upButtonIsDown, _downButtonIsDown;   //bool-variabel för om en given knapp är nertryckt eller släppt
        
        readonly int _playerSpeed = 5;    //sätter spelarens hastighet
        int _mobSpeed = 5;              //sätter mobens hastighet 

        //fields för bild-URIs
        readonly string _krakenLeft = "/Views/Components/Images/Happy_Kraken_Left.bmp";
        readonly string _krakenRight = "/Views/Components/Images/Happy_Kraken_Right.bmp";
        readonly string _pirateShip1Left = "/Views/Components/Images/PirateShip1_Left.bmp";
        readonly string _pirateShip1Right = "/Views/Components/Images/PirateShip1_Right.bmp.bmp";
        #endregion

        #region Constructors
        public GameView()
        {
            InitializeComponent();
            player.Focus();     //försöker fokusera på spelaren, behövs för KeyDown-event

            _timer.Interval = 10;    //sätter ett intervall i millisekunder för hur ofta MovePlayerEvent ska köras
            _timer.Elapsed += MovePlayerEvent;     //kör MovePlayerEvent varje gång interval ska börja om
            _timer.Elapsed += MoveMobEvent;     //kör MoveMobEvent varje gång interval ska börja om

            //_timer.Elapsed += MoveMobEvent;
            _timer.Start();    //startar timer
        }
        #endregion

        #region KeyEvents
        /// <summary>
        /// Fångar KeyDown-eventet när man trycker ner tangenten
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void player_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)   //I KeyEventArgs finns värdet "Key" som helt enkelt är vilken knapp som har tryckts ner, typ P, R, 8, Enter, NumLock etc. Switchar på det och sätter bool till true
            {
                case Key.A:        //här kan man ange typ vilka tangenter som helst, det ändrar kontrollen till spelet. Kanske ha userInput så man kan ändra själv?
                    _leftButtonIsDown = true;
                    break;

                case Key.D:
                    _rightButtonIsDown = true;
                    break;

                case Key.W:
                    _upButtonIsDown = true;
                    break;

                case Key.S:
                    _downButtonIsDown = true;
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Fångar KeyUp-eventet och stoppar spelarens rörelse när man släpper tangenten
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void player_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key) //switchar på e.Key och sätter booleans för *ButtonIsDown till false när man släpper tangent
            {
                case Key.A:
                    _leftButtonIsDown = false;
                    break;

                case Key.D:
                    _rightButtonIsDown = false;
                    break;

                case Key.W:
                    _upButtonIsDown = false;
                    break;

                case Key.S:
                    _downButtonIsDown = false;
                    break;

                default:
                    break;
            }
        }
        #endregion

        #region Methods for border check
        /// <summary>
        /// Kollar om en 'image' är tillräckligt långt från kanten eller inte.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="distanceToEdge"></param>
        /// <returns></returns>
        private bool IsNotAtLeftEdge(Image image, int distanceToEdge)
        {
            return Dispatcher.Invoke(() => Canvas.GetLeft(image) > distanceToEdge);
        }

        /// <summary>
        /// Kollar om en 'image' är tillräckligt långt från kanten eller inte.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="distanceToEdge"></param>
        /// <returns></returns>
        private bool IsNotAtRightEdge(Image image, int distanceToEdge)
        {
            return Dispatcher.Invoke(() => Canvas.GetLeft(image) + (image.Width) < (Application.Current.MainWindow.ActualWidth - 70) - distanceToEdge);
        }

        /// <summary>
        /// Kollar om en 'image' är tillräckligt långt från kanten eller inte.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="distanceToEdge"></param>
        /// <returns></returns>
        private bool IsNotAtTopEdge(Image image, int distanceToEdge)
        {
            return Dispatcher.Invoke(() => Canvas.GetTop(image) > distanceToEdge);
        }

        /// <summary>
        /// Kollar om en 'image' är tillräckligt långt från kanten eller inte.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="distanceToEdge"></param>
        /// <returns></returns>
        private bool IsNotAtBottomEdge(Image image, int distanceToEdge)
        {
            return Dispatcher.Invoke(() => Canvas.GetTop(image) + (image.Height) < (Application.Current.MainWindow.ActualHeight - 93) - distanceToEdge);
        }
        #endregion

        #region Movement
        /// <summary>
        /// Förflyttar spelare beroende på vilken tangentbordsknapp man trycker på
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MovePlayerEvent(object? sender, ElapsedEventArgs e)
        {
            if (_leftButtonIsDown && IsNotAtLeftEdge(player, 5))
            {
                SetLeftMovement(player, _playerSpeed); //sätter ny x-koordinat till den förra koordinaten MINUS _playerSpeed
                ChangeImage(player, _krakenLeft);        //koden gör att Kraken vänder på sig beroende på vilket håll den rör sig åt (vänster eller höger)
            }

            if (_rightButtonIsDown && IsNotAtRightEdge(player, 5))
            {
                SetLeftMovement(player, -_playerSpeed);   //sätter ny x-koordinat till den förra koordinaten PLUS _playerSpeed
                ChangeImage(player, _krakenRight);       //koden gör att Kraken vänder på sig beroende på vilket håll den rör sig åt (vänster eller höger)
            }

            if (_upButtonIsDown && IsNotAtTopEdge(player, 5))
            {
                SetTopMovement(player, _playerSpeed);       //sätter ny y-koordinat till den förra koordinaten MINUS _playerSpeed
            }

            if (_downButtonIsDown && IsNotAtBottomEdge(player, 5))
            {
                SetTopMovement(player, -_playerSpeed);      //sätter ny x-koordinat till den förra koordinaten PLUS _playerSpeed
            }
            CollisionCheckTest();
        }

        /// <summary>
        /// Sätter horizontell rörelse och hastighet
        /// </summary>
        /// <param name="imageComponent"></param>
        /// <param name="speed"></param>
        private void SetLeftMovement(Image imageComponent, int speed)
        {
            Dispatcher.Invoke(() => Canvas.SetLeft(imageComponent, Canvas.GetLeft(imageComponent) - speed));
        }

        /// <summary>
        /// Sätter vertikal rörelse och hastighet
        /// </summary>
        /// <param name="imageComponent"></param>
        /// <param name="speed"></param>
        private void SetTopMovement(Image image, int speed)
        {
            Dispatcher.Invoke(() => Canvas.SetTop(image, Canvas.GetTop(image) - speed));
        }

        /// <summary>
        /// Byter en bildkälla i UI till en ny
        /// </summary>
        /// <param name="image"></param>
        /// <param name="newImageURI"></param>
        private void ChangeImage(Image image, string newImageURI)
        {
            Dispatcher.Invoke(() => image.Source = new BitmapImage(new Uri(@newImageURI, UriKind.Relative)));   //byter imagesource till ny källa
        }

        private void MoveMobLeft(Image mobImage)
        {
            SetLeftMovement(mobImage, _mobSpeed);

            bool mobIsAtLeftEdge = !IsNotAtLeftEdge(pirateShip1, 5);
            bool mobIsAtRightEdge = !IsNotAtRightEdge(pirateShip1, 5);

            if (mobIsAtLeftEdge)
            {
                _mobSpeed = -_mobSpeed;   //byter håll 
            }
            if (mobIsAtRightEdge)
            {
                _mobSpeed = -_mobSpeed;   //byter håll
            }
        }

        private void CollisionCheckTest()    //Funkar men inte helt felfritt
        {
            bool collisionX = Dispatcher.Invoke(() => Canvas.GetLeft(player) < Canvas.GetLeft(pirateShip1) + (pirateShip1.Width)) && Dispatcher.Invoke(() => Canvas.GetLeft(player) + (player.Width) > Canvas.GetLeft(pirateShip1));
            bool collisionY = Dispatcher.Invoke(() => Canvas.GetTop(player) < Canvas.GetTop(pirateShip1) + (pirateShip1.Height)) && Dispatcher.Invoke(() => Canvas.GetTop(player) + (player.Height) > Canvas.GetTop(pirateShip1));

            if (collisionX && collisionY)
            {
                MessageBox.Show("The pirate ship was dragged down to the dark depths of the ocean, crushed by slimy tendrils and eaten for dinner");
            }
        }

        private void MoveMobEvent(object? sender, ElapsedEventArgs e)
        {
            MoveMobLeft(pirateShip1);
        } 
        #endregion
    }
}