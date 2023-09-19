using SUP23_G9.Commands;
using SUP23_G9.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace SUP23_G9.ViewModels
{
    public class TimerViewModel : BaseViewModel
    {
        private DispatcherTimer _timer;
        private int _remainingSeconds;
        public static bool _isNotAllowedToRunTimeUpEvents;
        public TimerViewModel(int initialSeconds)
        {
            _remainingSeconds = initialSeconds;
            _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            _timer.Tick += TimerTick;

            StartCommand = new RelayCommand(x => StartTimer());
            StopCommand = new RelayCommand(x => StopTimer());

            UpdateRemainingTime();
            _timer.Start();
        }

        public string RemainingTime { get; set; }
        
        public event EventHandler TimeUp; // Skapa en händelse för när tiden tar slut

        public ICommand StartCommand { get; private set; }
        public ICommand StopCommand { get; private set; }

        private void TimerTick(object sender, EventArgs e)
        {
            if (_remainingSeconds > 0)
            {
                _remainingSeconds--;
                UpdateRemainingTime();
                _isNotAllowedToRunTimeUpEvents = false;
            }
            else if (_remainingSeconds <= 0 && !_isNotAllowedToRunTimeUpEvents)
            {
                _isNotAllowedToRunTimeUpEvents = true;
                this.StopTimer();

                RemainingTime = "Time's up!";

                OnTimeUp(); // Trigga händelsen när tiden tar slut
            }
            else
            {
                return;
            }
        }

        public void StartTimer() => _timer.Start();

        public void StopTimer() => _timer.Stop();

        private void UpdateRemainingTime()
        {
            int minutes = _remainingSeconds / 60;
            int seconds = _remainingSeconds % 60;
            RemainingTime = $"{minutes:00 min}:{seconds:00 sec}";
        }

        // Metod för att triggas när tiden tar slut
        protected virtual void OnTimeUp()
        {
            TimeUp?.Invoke(this, EventArgs.Empty);
        }
    }
}
