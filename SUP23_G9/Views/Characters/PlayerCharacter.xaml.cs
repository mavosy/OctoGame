using SUP23_G9.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

namespace SUP23_G9.Views.Characters
{
    /// <summary>
    /// Interaction logic for PlayerCharacter.xaml
    /// </summary>
    public partial class PlayerCharacter : Character, INotifyPropertyChanged
    {
        private readonly Timer? _timer;

        Key _currentKey;
        public PlayerCharacter()
        {
            //LeftCoordinates = 0;
            //TopCoordinates = 0;

            //_timer = new Timer();
            //_timer.Interval = 100;
            //_timer.Elapsed += (_, _) => MovePlayerCharacter();
            //_timer.Start();


            //ArrowKeyDownCommand = new RelayCommand(x => ExecuteArrowKeyDown());
        }

        //metod som gör att något rör sig

        //Skapa ett relaycommand hanterar eventhandlers keydown tex
        //public ICommand ArrowKeyDownCommand { get; }
        public double LeftCoordinates { get; set; } = 50;
        public double TopCoordinates { get; set; } = 50;


        //private void Character_KeyDown(object sender, KeyEventArgs e)
        //{
        //    //_timer.Elapsed += (_, _) => MovePlayerCharacter(e.Key, characterSpeed: 20);

        //    switch (e.Key)
        //    {
        //        case Key.Left:
        //            //_currentKey = Key.Left;
        //            MessageBox.Show("Vänster");
        //            break;
        //        case Key.Right:
        //            _currentKey = Key.Right;
        //            break;
        //        case Key.Up:
        //            _currentKey = Key.Up;
        //            break;
        //        case Key.Down:
        //            _currentKey = Key.Down;
        //            break;
        //    }
        //}
        //private void ExecuteArrowKeyDown()
        //{
        //    if (obj is Key key)
        //    {
        //        if (key == currentKey)
        //        {
        //            // The same key is pressed again, so stop the timer to prevent rapid movements
        //            _timer.Stop();
        //            currentKey = Key.None; // Reset the current key
        //        }
        //        else
        //        {
        //            // Start the timer when the arrow key is pressed
        //            currentKey = key;
        //            _timer.Start();
        //        }
        //    }
        //}

        //private Key _currentKey; // To keep track of the last pressed key

        //public void MovePlayerCharacter(Key key)
        //{
        //    //Key currentKey = key;
        //    //if (key == currentKey)
        //    //{
        //    //    _timer.Stop();
        //    //    currentKey = Key.None;
        //    //}
        //    //else
        //    //{
        //    //    _timer.Start();

        //    //int characterSpeed = 20;
        //    //switch (key)
        //    //{
        //    //    case Key.A:
        //    //        MessageBox.Show("Funkar");
        //    //        //LeftCoordinates -= characterSpeed;
        //    //        break;
        //    //    case Key.D:
        //    //        LeftCoordinates += characterSpeed;
        //    //        break;
        //    //    case Key.W:
        //    //        TopCoordinates -= characterSpeed;
        //    //        break;
        //    //    case Key.S:
        //    //        TopCoordinates += characterSpeed;
        //    //        break;
        //    //}
        //}
    }
}