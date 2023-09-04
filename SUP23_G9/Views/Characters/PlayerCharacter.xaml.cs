using SUP23_G9.Commands;
using System;
using System.Collections.Generic;
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
    public partial class PlayerCharacter : Character
    {
        private readonly Timer? _timer;

        public PlayerCharacter()
        {
            //LeftCoordinates = 0;
            //TopCoordinates = 0;

            _timer = new Timer();
            _timer.Interval = 100;
 //           _timer.Elapsed += (_, _) => MovePlayerCharacter();

            //ArrowKeyDownCommand = new RelayCommand(x => ExecuteArrowKeyDown());
        }
        
        //Skapa ett relaycommand hanterar eventhandlers keydown tex
        //public ICommand ArrowKeyDownCommand { get; }
        public int LeftCoordinates { get; set; } = 0;
        public int TopCoordinates { get; set; } = 0;


        private void Character_KeyDown(object sender, KeyEventArgs e)
        {
            _timer.Elapsed += (_, _) => MovePlayerCharacter(e.Key, characterSpeed: 20);
        }
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

        private void MovePlayerCharacter(Key key, int characterSpeed)
        {
            Key currentKey = key;
            if (key == currentKey)
            {
                _timer.Stop();
                currentKey = Key.None;
            }
            else
            {
                switch (currentKey)
                {
                    case Key.Left:
                        LeftCoordinates -= characterSpeed;
                        break;
                    case Key.Right:
                        LeftCoordinates += characterSpeed;
                        break;
                    case Key.Up:
                        TopCoordinates -= characterSpeed;
                        break;
                    case Key.Down:
                        TopCoordinates += characterSpeed;
                        break;
                }

            }
        }

    }
}