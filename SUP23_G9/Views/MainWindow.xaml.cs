using SUP23_G9.ViewModels;
using SUP23_G9.Views.Characters;
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
    public partial class MainWindow : Window
    {

        //    bool goLeft, goRight, goUp, goDown;
        //    int playerSpeed = 10; //Spelarens hastighet
        //    int speed = 6; //Mobs hastighet
        //    int score = 0; //Poängsättare++
        //    int count = 3; //Nedräkning från 3

        //    DispatcherTimer gameTimer = new DispatcherTimer();
        //    DispatcherTimer countTimer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();

            //        InitializeComponent();
            //        StartGame();
            //        showCount();
            //        myCanvas.Focus(); //Fokus på canvasen och spelare

            //        //tillhör dispatcher timer som får saker att röra på sig
            //        //https://learn.microsoft.com/en-us/dotnet/api/system.windows.threading.dispatchertimer?view=windowsdesktop-7.0
            //        gameTimer.Tick += GameTimerEvent; //+= länkar till event
            //        gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            //        gameTimer.Start();

            //        countTimer.Tick += countTimerTick;
            //        countTimer.Interval = TimeSpan.FromSeconds(1);

        }
    }

        //    private void showCount()
        //    {
        //        startMenu.Visibility = Visibility.Visible;
        //        countTimer.Start();
        //    }

        //    //Nedräkning för spelstart
        //    private void countTimerTick(object sender, EventArgs e)
        //    {
        //        startMenu.Text = count.ToString();

        //        if(count == 0)
        //        {
        //            countTimer.Stop();
        //            startMenu.Visibility = Visibility.Hidden;
        //        }

        //        else
        //        {
        //            count--;
        //        }
        //    }

        //    // Startruta
        //    private bool StartGame()
        //    {
        //        MessageBoxResult messageBoxResult = MessageBox.Show("Vill du starta spelet?", "Starta spelet", MessageBoxButton.YesNo);
        //        return messageBoxResult == MessageBoxResult.Yes;
        //    }


        //    // Event handler som gör det möjligt för spelaren att kunna röra på sig
        //    // Spelaren ska ej kunna röra sig utanför canvas
        //    private void GameTimerEvent(object? sender, EventArgs e) 

        //    {
        //        if (goLeft == true && Canvas.GetLeft(player) > 5)
        //        {
        //            Canvas.SetLeft(player, Canvas.GetLeft(player) - playerSpeed);
        //        }

        //        if (goRight == true && Canvas.GetLeft(player) + (player.Width + 20) < Application.Current.MainWindow.Width)
        //        {
        //            Canvas.SetLeft(player, Canvas.GetLeft(player) + playerSpeed);
        //        }

        //        if (goUp == true && Canvas.GetTop(player) > 5)
        //        {
        //            Canvas.SetTop(player, Canvas.GetTop(player) - playerSpeed);
        //        }

        //        if (goDown == true && Canvas.GetTop(player) + (player.Height * 2) < Application.Current.MainWindow.Height)
        //        {
        //            Canvas.SetTop(player, Canvas.GetTop(player) + playerSpeed);
        //        }

        //        //if(count == 0)
        //        // {            Rectangle[] mobz = new Rectangle[] { mob, mob2, mob3, mob4, evilmob, evilmob2 };

        //        // foreach(var mob in mobz)
        //        // {
        //        //     //Ska röra sig vertikalt därav använder SetTop/GetTop
        //        //     Canvas.SetTop(mob, Canvas.GetTop(mob) + speed);

        //        //     if (Canvas.GetTop(mob) + (mob.Height * 2) > Application.Current.MainWindow.Height)
        //        //         {
        //        //         Canvas.SetTop(mob, 0); //Ny position 0 dvs toppen
        //        //         }
        //        // }

        //        // }


        //        //Uppdaterad nu med Ship/Obstacle som objekt + använder metod. Gör så att skeppen/hinder rör sig.
        //        if (count == 0)
        //        {
        //            Ship[] ships = new Ship[] { mob, mob2, mob3, mob4 };
        //            Obstacle[] obstacles = new Obstacle[] { evilmob, evilmob2, evilmob3 };

        //            foreach (var ship in ships)
        //            {
        //                MoveShip(ship, speed);
        //            }

        //            foreach (var obstacle in obstacles)
        //            {
        //                MoveObstacle(obstacle, speed);
        //            }
        //        }


        //        //Upptäcker kollision med skepp
        //        foreach (var x in myCanvas.Children.OfType<Ship>())
        //        {
        //            if ((string)x.Tag == "mob" && x.Visibility == Visibility.Visible) // Reagerar på allt med tag "mob"
        //            {
        //                Rect playerHit = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.Width, player.Height);
        //                Rect hitShip = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.ActualWidth, x.ActualHeight);

        //                if (playerHit.IntersectsWith(hitShip))
        //                {
        //                    x.Visibility = Visibility.Hidden; 
        //                    score++;
        //                    scoreBoard.Content = "Poäng: " + score + " av 4";

        //                    if (score == 4)
        //                        { 
        //                            MessageBox.Show("Grattis! Du åt alla 4 skeppen");
        //                            playerSpeed = 0;
        //                        }
        //                }
        //            }  
        //        }

        //        // Upptäcker kollision med hinder
        //        foreach (var x in myCanvas.Children.OfType<Obstacle>())

        //            if ((string)x.Tag == "evilmob")
        //            {
        //                Rect playerHit = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.Width, player.Height);
        //                Rect hitObstacle = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.ActualWidth, x.ActualHeight);

        //                if (playerHit.IntersectsWith(hitObstacle))
        //                {
        //                    MessageBox.Show("You lost!");
        //                    Close(); // Bättre om visar ruta så att spelaren kan restarta igen
        //                }
        //            }
        //        }

        //    //Metod som gör det möjligt så att hinder rör sig vertikalt och "loopar"
        //    private void MoveObstacle(Obstacle mob, int speed)
        //    {
        //        double top = Canvas.GetTop(mob); //Hämtar nuvarande position av mob i canvasen och tilldelar variabel top
        //        top += speed;
        //        Canvas.SetTop(mob, top); //"Sätter" ny position för mob i canvasen

        //        if (top > myCanvas.ActualHeight) //OM den nya positionen av mob/top går utanför canvasens höjd = true 
        //        {
        //            top = 0; //Sätter tillbaka mob på toppen av canvas dvs 0
        //        }

        //        Canvas.SetTop(mob, top); //Uppdaterar position
        //    }

        //    //Metod som gör det möjligt så att skeppen rör sig vertikalt och "loopar"
        //    private void MoveShip(Ship mob, int speed)
        //    {
        //        double top = Canvas.GetTop(mob); //Hämtar nuvarande position av mob i canvasen och tilldelar variabel top
        //        top += speed;
        //        Canvas.SetTop(mob, top); //"Sätter" ny position för mob i canvasen

        //        if (top > myCanvas.ActualHeight) //OM den nya positionen av mob/top går utanför canvasens höjd = true 
        //        {
        //            top = 0; //Sätter tillbaka mob på toppen av canvas dvs 0
        //        }

        //        Canvas.SetTop(mob, top); //Uppdaterar position
        //    }

        //    //Kontroll för riktning när tangenterna AWSD är nedtryckta
        //    private void KeyIsDown(object sender, KeyEventArgs e) 
        //    {

        //        switch (e.Key)
        //        {
        //            case Key.A:
        //                goLeft = true;
        //                break;

        //            case Key.D:
        //                goRight= true;
        //                break;

        //            case Key.W:
        //                goUp = true;
        //                break;

        //            case Key.S:
        //                goDown = true;
        //                break;

        //                default:
        //                break;
        //        }
        //    }

        //    //Kontroll för riktning när tangenterna AWSD ej är nedtryckta
        //    private void KeyIsUp(object sender, KeyEventArgs e) 
        //    {
        //        switch (e.Key)
        //        {
        //            case Key.A:
        //                goLeft = false;
        //                break;

        //            case Key.D:
        //                goRight = false;
        //                break;

        //            case Key.W:
        //                goUp = false;
        //                break;

        //            case Key.S:
        //                goDown = false;
        //                break;

        //            default:
        //                break;
        //        }
        //    }
        //}
    }