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
        /// <summary>
        /// X-koordinat för skeppen
        /// </summary>
        public double Left { get; set; }
        /// <summary>
        /// Y-koordinat för skeppen
        /// </summary>
        public double Top { get; set; }
    }
}