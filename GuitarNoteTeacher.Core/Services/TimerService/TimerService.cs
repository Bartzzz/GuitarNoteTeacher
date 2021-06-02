using GuitarNoteTeacher.Core.Events;
using Prism.Events;
using System;
using System.Windows.Threading;

namespace GuitarNoteTeacher.Core.Services.TimerService
{
    public class TimerService: ITimerService
    {
        public int GameTime => 90;

        private DispatcherTimer _timer;
        private IEventAggregator _eventAggregator;


        public TimerService(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            CreateTimer();
        }

        private void CreateTimer()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
        }

        public void StartTimer()
        {
            if (!_timer.IsEnabled)
            {
                _timer.Start();
            }

            _timer.Tick += OnTimerTick;
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            _eventAggregator.GetEvent<TimerTickEvent>().Publish();
   
        }

        public int AddTimeBonus(int Time)
        {
            var bonus = 10;
            var timeWithBonus = Time + bonus;

            if (timeWithBonus >= 110)
            {
                Time = 110;
            }
            else
            {
                Time = timeWithBonus;
            }

            return Time;
        }

        public void StopTimer()
        {
            if(_timer.IsEnabled)
            {
                _timer.Stop();
                _timer.Tick -= OnTimerTick;
            }
        }
    }
}
