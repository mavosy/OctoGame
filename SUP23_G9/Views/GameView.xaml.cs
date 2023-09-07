using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading; //dispatcher timer

namespace SUP23_G9.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class GameView : UserControl
    {

        bool goLeft, goRight, goUp, goDown;
        int playerSpeed = 10; //spelarens hastighet
        int speed = 6; //Mobs hastighet
        int score = 0; //Poängsättare

        int count = 3;

        DispatcherTimer gameTimer = new DispatcherTimer();
        DispatcherTimer countTimer = new DispatcherTimer();

        public GameView()
        {
            InitializeComponent();
            StartGame();
            showCount();
            myCanvas.Focus(); //Fokus på canvasen och spelare

            //tillhör dispatcher timer som får saker att röra på sig
            //https://learn.microsoft.com/en-us/dotnet/api/system.windows.threading.dispatchertimer?view=windowsdesktop-7.0
            gameTimer.Tick += GameTimerEvent; //+= länkar till event
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            gameTimer.Start();

            countTimer.Tick += countTimerTick;
            countTimer.Interval = TimeSpan.FromSeconds(1);

        }

        private void showCount()
        {
            startMenu.Visibility = Visibility.Visible;
            countTimer.Start();
        }

        private void countTimerTick(object sender, EventArgs e)
        {
            startMenu.Text = count.ToString();

            if (count == 0)
            {
                countTimer.Stop();
                startMenu.Visibility = Visibility.Hidden;
            }

            else
            {
                count--;
            }
        }

        private bool StartGame()
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Vill du starta spelet?", "Starta spelet", MessageBoxButton.YesNo);
            return messageBoxResult == MessageBoxResult.Yes;
        }


        //Spelaren kan röra på sig top/down/left/right
        private void GameTimerEvent(object? sender, EventArgs e)

        {
            if (goLeft == true && Canvas.GetLeft(player) > 5)
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) - playerSpeed);
            }

            if (goRight == true && Canvas.GetLeft(player) + (player.Width + 20) < Application.Current.MainWindow.Width)
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) + playerSpeed);
            }

            if (goUp == true && Canvas.GetTop(player) > 5)
            {
                Canvas.SetTop(player, Canvas.GetTop(player) - playerSpeed);
            }

            if (goDown == true && Canvas.GetTop(player) + (player.Height * 2) < Application.Current.MainWindow.Height)
            {
                Canvas.SetTop(player, Canvas.GetTop(player) + playerSpeed);
            }

            if (count == 0)
            {
                Rectangle[] mobz = new Rectangle[] { mob, mob2, mob3, mob4, evilmob, evilmob2 };

                foreach (var mob in mobz)
                {
                    //Ska röra sig vertikalt därav använder SetTop/GetTop
                    Canvas.SetTop(mob, Canvas.GetTop(mob) + speed);

                    if (Canvas.GetTop(mob) + (mob.Height * 2) > Application.Current.MainWindow.Height)
                    {
                        Canvas.SetTop(mob, 0); //Ny position 0 dvs toppen
                    }
                }

            }


            //Upptäcker kollision
            foreach (var x in myCanvas.Children.OfType<Rectangle>())
            {
                if ((string)x.Tag == "mob") // reagerar på allt med tag mob
                {

                    Rect playerHitBox = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.Width, player.Height);
                    Rect platformHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    if (playerHitBox.IntersectsWith(platformHitBox) && x.Visibility == Visibility.Visible)
                    {
                        x.Visibility = Visibility.Hidden; // Ska ha nått annat än hidden?
                        score++;
                        scoreBoard.Content = "Poäng: " + score + " av 4";
                        playerSpeed = 0;

                        if (score == 4)
                        {
                            MessageBox.Show("Grattis! Du åt alla 4 skeppen");

                        }
                    }
                }
            }

            // Upptäcker kollision med evil mob
            foreach (var x in myCanvas.Children.OfType<Rectangle>())
                if ((string)x.Tag == "evilmob")
                {
                    Rect playerHitBox = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.Width, player.Height);
                    Rect evilmobs = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    if (playerHitBox.IntersectsWith(evilmobs))
                    {
                        MessageBox.Show("You lost!");
                        //Close(); // Bättre om visar total poäng och så att spelaren kan restarta igen
                    }

                    else
                    {
                        playerSpeed = 10;
                    }
                }
        }

        private void KeyIsDown(object sender, KeyEventArgs e) //Riktningar när tangent (arrow keys) nedtryckt
        {

            switch (e.Key)
            {
                case Key.A:
                    goLeft = true;
                    break;

                case Key.D:
                    goRight = true;
                    break;

                case Key.W:
                    goUp = true;
                    break;

                case Key.S:
                    goDown = true;
                    break;

                default:
                    break;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e) //När piltangenter ej används/ej nedtryckta
        {
            switch (e.Key)
            {
                case Key.A:
                    goLeft = false;
                    break;

                case Key.D:
                    goRight = false;
                    break;

                case Key.W:
                    goUp = false;
                    break;

                case Key.S:
                    goDown = false;
                    break;

                default:
                    break;
            }
        }
    }
}
