using SUP23_G9.Commands;
using SUP23_G9.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace SUP23_G9.ViewModels
{
    internal class TimerViewModel: BaseViewModel
    {
        private DispatcherTimer _timer;
        private int _remainingSeconds;
        private string _remainingTime;

        public TimerViewModel(int initialSeconds)
        {
            _remainingSeconds = initialSeconds;
            _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            _timer.Tick += TimerTick;

            StartCommand = new RelayCommand(x =>StartTimer());
            StopCommand = new RelayCommand(x => StopTimer());

            UpdateRemainingTime();
        }

        public string RemainingTime
        {
            get => _remainingTime;
            set
            {
                _remainingTime = value;
            }
        }

        public ICommand StartCommand { get; private set; }
        public ICommand StopCommand { get; private set; }

        private void TimerTick(object sender, EventArgs e)
        {
            if (_remainingSeconds > 0)
            {
                _remainingSeconds--;
                UpdateRemainingTime();
            }
            else
            {
                _timer.Stop();
                RemainingTime = "Time's up!";
            }
        }

        private void StartTimer() => _timer.Start();

        private void StopTimer() => _timer.Stop();

        private void UpdateRemainingTime()
        {
            int minutes = _remainingSeconds / 60;
            int seconds = _remainingSeconds % 60;
            RemainingTime = $"{minutes:00 min}:{seconds:00 sec}";
        }
    }
}

