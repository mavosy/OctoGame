using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUP23_G9.ViewModels.Base
{
    public class ShipViewModel : BaseViewModel
    {
        public ShipViewModel()
        {
            Width = 50;
            Height = 50;
        }
        /// <summary>
        /// X-koordinat för skeppen
        /// </summary>
        public double Left { get; set; }
        /// <summary>
        /// Y-koordinat för skeppen
        /// </summary>
        public double Top { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }
    }
}