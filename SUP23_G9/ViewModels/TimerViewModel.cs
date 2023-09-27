using SUP23_G9.ViewModels.Base;
using System;
using System.Diagnostics;
using System.Windows.Threading;

namespace SUP23_G9.ViewModels
{
    public class TimerViewModel : BaseViewModel
    {
        private const int secondsInMinute = 60;
        public DispatcherTimer _timer;
        private int _remainingSeconds;

        /// <summary>
        /// Initierar en ny instans av TimerViewModel med specificerade initiala sekunder.
        /// </summary>
        public TimerViewModel(int initialSeconds)
        {
            _remainingSeconds = initialSeconds;
            _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            _timer.Tick += TimerTick;
            UpdateRemainingTime();
            _timer.Start();
        }

        /// <summary>
        /// Representerar den återstående tiden som en sträng.
        /// </summary>
        public string RemainingTime { get; private set; }

        /// <summary>
        /// Hanterar Timer Tick eventet. Uppdaterar den återstående tiden eller avslutar timern om tiden är slut.
        /// </summary>
        public void TimerTick(object sender, EventArgs e)
        {
            Debug.WriteLine($"TimerViewModel event fire with ID: {InstanceID}");
            if (_remainingSeconds > 0)
            {
                _remainingSeconds--;
                UpdateRemainingTime();
            }
            else
            {
                CompleteTimer();
            }
        }

        /// <summary>
        /// Uppdaterar RemainingTime property med rätt format.
        /// </summary>
        private void UpdateRemainingTime()
        {
            int minutes = _remainingSeconds / secondsInMinute;
            int seconds = _remainingSeconds % secondsInMinute;
            RemainingTime = $"{minutes:00 min}:{seconds:00 sek}";
        }

        /// <summary>
        /// Markerar att timern är klar och triggar TimeUpHandler eventet.
        /// </summary>
        private void CompleteTimer()
        {
            RemainingTime = "Tiden är ute!";
            RaiseTimeUpHandler();
        }

        /// <summary>
        /// Eventet som triggas när tiden är ute.
        /// </summary>
        public event EventHandler TimeUpHandler;

        /// <summary>
        /// Triggar TimeUpHandler eventet.
        /// </summary>
        private void RaiseTimeUpHandler()
        {
            TimeUpHandler?.Invoke(this, EventArgs.Empty);
        }
    }
}