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
    //            // Kollar om knappen trycks ner igen
    //            _timer.Stop();
    //            currentKey = Key.None; // Resetta knapp
    //        }
    //        else
    //        {
    //            // Startar timer
    //            currentKey = key;
    //            _timer.Start();
    //        }
    //    }
    //}

    //private void MovePlayerCharacter()
    //{
    //    int speed = 10; // hastighet

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

    //private Key currentKey; // håller vilken knapp som trycks ner
}
}
