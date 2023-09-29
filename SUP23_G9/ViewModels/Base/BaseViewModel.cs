using PropertyChanged;
using System;

namespace SUP23_G9.ViewModels.Base
{
    [AddINotifyPropertyChangedInterface]

    public abstract class BaseViewModel
    {
        /// <summary>
        /// Skapar unikt ID för varje klass-instans som ärver från BaseViewModel. Ger möjlighet att identifiera enskilda instanser av klasser med t ex Debug.Writeline
        /// </summary>
        public string InstanceID { get; set; } = Guid.NewGuid().ToString();
    }
}