using SUP23_G9.Commands;
using SUP23_G9.ViewModels.Base;
using System;
using System.Diagnostics;
using System.Windows.Input;
using System.Windows.Threading;

namespace SUP23_G9.ViewModels
{
    public class TimerViewModel : BaseViewModel
    {
        public DispatcherTimer _timer;
        public int _remainingSeconds;

        //TODO Ta bort tom konstruktor?
        public TimerViewModel()
        {

        }
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
        public ICommand StartCommand { get; private set; }
        public ICommand StopCommand { get; private set; }

        public event EventHandler TimeUp; // Skapa en händelse för när tiden tar slut
        public void TimerTick(object sender, EventArgs e)
        {
            Debug.WriteLine($"TimerViewModel event fire with ID: {InstanceID}");
            if (_remainingSeconds > 0)
            {
                _remainingSeconds--;
                UpdateRemainingTime();
            }
            else if (_remainingSeconds <= 0)
            {
                RemainingTime = "Time's up!";

                this.StopTimer();

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
        public void OnTimeUp()
        {
            TimeUp?.Invoke(this, EventArgs.Empty);
        }
    }
}