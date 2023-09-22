using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace SUP23_G9.ViewModels.Base
{
    [AddINotifyPropertyChangedInterface]

    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public string InstanceID { get; } = Guid.NewGuid().ToString();

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}