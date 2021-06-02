using GuitarNoteTeacher.Core.Events;
using GuitarNoteTeacher.Core.Services.PointsService;
using GuitarNoteTeacher.Core.Services.PointsService.Objects;
using GuitarNoteTeacher.Core.Services.TimerService;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System.Windows;
using System.Windows.Input;

namespace GuitarNoteTeacher.UI.ResultsModule.ViewModels
{
    public class ResultsModuleViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private int _goodAnswersCount;
        private ITimerService _timerService { get; set; }
        private int _bonusDisplayTime;
        private int _time;
        public int Time
        {
            get => _time;
            set { SetProperty(ref _time, value); }
        }

        private int _points;
        public int Points
        {
            get => _points;
            set { SetProperty(ref _points, value); }
        }

        private Visibility _bonusVisible;
        private IPointsService _pointsService;

        public Visibility BonusVisible
        {
            get => _bonusVisible;
            set { SetProperty(ref _bonusVisible, value); }
        }
        private HighScore _currentHighScore;
        public HighScore CurrentHighScore
        {
            get { return _currentHighScore; }
            set { SetProperty(ref _currentHighScore, value); }
           
        }

        public ResultsModuleViewModel(IEventAggregator eventAggregator, ITimerService timerService, IPointsService pointsService)
        {
            _eventAggregator = eventAggregator;
            _timerService = timerService;
            _pointsService = pointsService;

            _eventAggregator.GetEvent<GameStartEvent>().Subscribe(OnGameStartEventReceived);
            _eventAggregator.GetEvent<TimerTickEvent>().Subscribe(OnTimerTick);
            _eventAggregator.GetEvent<AnsweredEvent>().Subscribe(OnAnswerReceived);

            CurrentHighScore = _pointsService.GetCurrentHighScore();
            BonusVisible = Visibility.Hidden;
        }

        private void OnAnswerReceived(bool answer)
        {
            if (answer)
            {
                _goodAnswersCount++;

                Points += _pointsService.AddPoints(_goodAnswersCount);

                if (_goodAnswersCount >= 5 && _goodAnswersCount % 5 == 0)
                {
                    AddTimeBonus();
                }
            }
            else
            {
                _goodAnswersCount = 0;
            }
        }

        private void AddTimeBonus()
        {
            Time = _timerService.AddTimeBonus(Time);
            _bonusDisplayTime += 3;
            BonusVisible = Visibility.Visible;
        }

        private void OnTimerTick()
        {
            if (_bonusDisplayTime > 0)
            {
                _bonusDisplayTime--;
            }
            else
            {
                BonusVisible = Visibility.Hidden;
            }

            Time -= 1;
            if (Time <= 0)
            {
                OnTimerElapsed();
            }
        }

        private void OnGameStartEventReceived()
        {
            _timerService.StopTimer();
            CurrentHighScore = _pointsService.GetCurrentHighScore();
            Points = 0;
            Time = _timerService.GameTime;
            _timerService.StartTimer();
        }

        private void OnTimerElapsed()
        {
            var highScore = new HighScore();
            highScore.Points = Points;

            if (Points > CurrentHighScore.Points)
            {
                highScore.IsHighScore = true;
            }
            else
            {
                highScore.IsHighScore = false;
            }

            _timerService.StopTimer();
            _eventAggregator.GetEvent<GameOverEvent>().Publish(highScore);
        }
    }
}
