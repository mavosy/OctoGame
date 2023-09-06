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
        public PlayerCharacter()
        {

        }

        //dessa properties binder till playerCharacter UC i GameView UI
        public double LeftCoordinates { get; set; } = 50; 
        public double TopCoordinates { get; set; } = 50;
    }
}