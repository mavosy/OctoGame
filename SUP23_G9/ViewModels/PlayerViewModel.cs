using SUP23_G9.Commands;
using SUP23_G9.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;

namespace SUP23_G9.ViewModels
{
    class PlayerViewModel : BaseViewModel
    {
        //private readonly Timer? _timer;

        //public PlayerViewModel()
        //{
        //    LeftCoordinates = 0;
        //    TopCoordinates = 0;

        //    _timer = new Timer();
        //    _timer.Interval = 100;
        //    _timer.Elapsed += (_, _) => MovePlayerCharacter();

        //    //ArrowKeyDownCommand = new RelayCommand(x => ExecuteArrowKeyDown());
        //}

        ////Skapa ett relaycommand hanterar eventhandlers keydown tex
        //public ICommand ArrowKeyDownCommand { get; }
        //public double LeftCoordinates { get; set; }
        //public double TopCoordinates { get; set; }


        //private void ExecuteArrowKeyDown(object obj)
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

        //private void MovePlayerCharacter()
        //{
        //    int speed = 10; // Adjust this for your desired movement speed

        //    switch (currentKey)
        //    {
        //        case Key.Left:
        //            LeftCoordinates -= speed;
        //            break;
        //        case Key.Right:
        //            LeftCoordinates += speed;
        //            break;
        //        case Key.Up:
        //            TopCoordinates -= speed;
        //            break;
        //        case Key.Down:
        //            TopCoordinates += speed;
        //            break;
        //    }
        //}

        //private Key currentKey; // To keep track of the last pressed key
    }
}
