using SUP23_G9.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUP23_G9.ViewModels
{
    class GameViewModel : BaseViewModel
    {
        public PlayerViewModel Player1 { get; set; } = new HumanPlayerViewModel();
        public PlayerViewModel Player2 { get; set; } = new HumanPlayerViewModel();

    }
}