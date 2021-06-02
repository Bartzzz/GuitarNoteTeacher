using GuitarNoteTeacher.Core.Events;
using GuitarNoteTeacher.Core.Services.PointsService.Objects;
using GuitarNoteTeacher.Core.Services.TimerService;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using   System.Windows;

namespace GuitarNoteTeacher.UI.InformationModule.ViewModels
{
    public class InformationModuleViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ITimerService _timerService;

        public DelegateCommand StartGameCommand { get; set; }

        private Visibility _startButtonVisble;
        public Visibility StartButtonVisible 
        {
            get { return _startButtonVisble; }
            set {SetProperty(ref _startButtonVisble, value);}
        }

        private int _answerCorrectnessButtonTimer;

        private Visibility _correctAnswerButtonVisble;
        public Visibility CorrectAnswerButtonVisible
        {
            get { return _correctAnswerButtonVisble; }
            set { SetProperty(ref _correctAnswerButtonVisble, value); }
        }

        private Visibility _wrongAnswerButtonVisble;
        public Visibility WrongAnswerButtonVisible
        {
            get { return _wrongAnswerButtonVisble; }
            set { SetProperty(ref _wrongAnswerButtonVisble, value); }
        }

        public InformationModuleViewModel(IEventAggregator eventAggregator, ITimerService timerService)
        {
            _eventAggregator = eventAggregator;
            _timerService = timerService;
            StartGameCommand = new DelegateCommand(StartGame);
            _eventAggregator.GetEvent<GameOverEvent>().Subscribe(ResetGame);
            _eventAggregator.GetEvent<AnsweredEvent>().Subscribe(OnAnswered);
            _eventAggregator.GetEvent<TimerTickEvent>().Subscribe(OnTimerTick);

            HideAnswerButtons();
        }

        private void OnTimerTick()
        {
            if (_answerCorrectnessButtonTimer > 0)
            {
                _answerCorrectnessButtonTimer--;
            }
            else if(CorrectAnswerButtonVisible == Visibility.Visible || WrongAnswerButtonVisible == Visibility.Visible)
            {
                HideAnswerButtons();
            }
        }

        private void OnAnswered(bool answer)
        {
            if(answer)
            {
                HideAnswerButtons();
                _answerCorrectnessButtonTimer += 1;
                CorrectAnswerButtonVisible = Visibility.Visible;
            }
            else
            {
                HideAnswerButtons();
                _answerCorrectnessButtonTimer += 1;
                WrongAnswerButtonVisible = Visibility.Visible;
            }
        }

        private void ResetGame(HighScore highScore)
        {
            HideAnswerButtons();
            StartButtonVisible = Visibility.Visible;
        }

        private void StartGame()
        {
            _eventAggregator.GetEvent<GameStartEvent>().Publish();
            StartButtonVisible = Visibility.Hidden;
        }
        private void HideAnswerButtons()
        {
            _answerCorrectnessButtonTimer = 0;
            CorrectAnswerButtonVisible = Visibility.Hidden;
            WrongAnswerButtonVisible = Visibility.Hidden;
        }
    }
}
