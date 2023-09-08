using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUP23_G9.ViewModels
{
    internal class TimerViewModel: INotifyPropertyChanged
    {
        private DispatcherTimer _timer;
        private int _remainingSeconds;
        private string _remainingTime;

        public event PropertyChangedEventHandler PropertyChanged;

        public TimeViewModel(int initialSeconds)
        {
            _remainingSeconds = initialSeconds;
            _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            _timer.Tick += Timer_Tick;

            StartCommand = new BaseViewModel(StartTimer);
            StopCommand = new BaseViewModel(StopTimer);

            UpdateRemainingTime();
        }

        public string RemainingTime
        {
            get => _remainingTime;
            set
            {
                _remainingTime = value;
                OnPropertyChanged(nameof(RemainingTime));
            }
        }

        public ICommand StartCommand { get; }
        public ICommand StopCommand { get; }

        private void Timer_Tick(object sender, EventArgs e)
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

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
}
