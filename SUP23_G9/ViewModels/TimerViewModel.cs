using SUP23_G9.ViewModels.Base;
using System;
using System.Diagnostics;
using System.Windows.Threading;

namespace SUP23_G9.ViewModels
{
    public class TimerViewModel : BaseViewModel
    {
        public DispatcherTimer _timer;
        public int _remainingSeconds;

        public TimerViewModel(int initialSeconds)
        {
            _remainingSeconds = initialSeconds;
            _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            _timer.Tick += TimerTick;
            UpdateRemainingTime();
            _timer.Start();
        }

        public string RemainingTime { get; private set; }

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

        private void UpdateRemainingTime()
        {
            int minutes = _remainingSeconds / 60;
            int seconds = _remainingSeconds % 60;
            RemainingTime = $"{minutes:00 min}:{seconds:00 sec}";
        }

        private void CompleteTimer()
        {
            RemainingTime = "Time's up!";

            RaiseTimeUpEvent();
        }

        public event EventHandler TimeUpEvent;
        private void RaiseTimeUpEvent()
        {
            TimeUpEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}