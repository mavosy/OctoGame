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

        public DispatcherTimer timer;
        public int remainingSeconds;

       
        public TimerViewModel(int initialSeconds)
        {
            remainingSeconds = initialSeconds;
            timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            timer.Tick += TimerTick;
            UpdateRemainingTime();
            timer.Start();
        }
        public string RemainingTime { get; private set; }

        public ICommand StartCommand { get; }
        public ICommand StopCommand { get; }


        public event EventHandler TimeUpEvent; 
       
        public void TimerTick(object sender, EventArgs e)
        {
            Debug.WriteLine($"TimerViewModel event fire with ID: {InstanceID}");
            if (remainingSeconds > 0)
            {
                remainingSeconds--;
                UpdateRemainingTime();
            }
            else
            {
                CompleteTimer();
            }

        }
        private void UpdateRemainingTime()
        {
            int minutes = remainingSeconds / 60;
            int seconds = remainingSeconds % 60;
            RemainingTime = $"{minutes:00 min}:{seconds:00 sec}";
        }
        private void CompleteTimer()
        {
            RemainingTime = "Time's up!";
          
            RaiseTimeUpEvent();
        }

        private void RaiseTimeUpEvent() => TimeUpEvent?.Invoke(this, EventArgs.Empty);
       

       
       
    }
}