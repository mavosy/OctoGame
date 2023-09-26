using PropertyChanged;
using System;

namespace SUP23_G9.ViewModels.Base
{
    [AddINotifyPropertyChangedInterface]

    public abstract class BaseViewModel
    {
        public string InstanceID => Guid.NewGuid().ToString();
    }
}