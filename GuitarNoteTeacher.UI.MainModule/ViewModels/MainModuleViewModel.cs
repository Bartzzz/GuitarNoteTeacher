using GuitarNoteTeacher.Core.Events;
using GuitarNoteTeacher.Core.Services.NotesService;
using GuitarNoteTeacher.Core.Services.PointsService.Objects;
using GuitarNoteTeacher.Core.Services.TimerService;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace GuitarNoteTeacher.UI.MainModule.ViewModels
{
    public class MainModuleViewModel : BindableBase
    {
        public List<Fret> FretsList { get; set; }
        public DelegateCommand<string> ButtonClickedCommand { get; private set; }
        public DelegateCommand<KeyEventArgs> KeyDownEventCommand { get; private set; }

        public DelegateCommand<KeyboardFocusChangedEventArgs> LostFocusCommand { get; private set; }

        private bool _gameStarted;
        public bool GameStarted
        {
            get { return _gameStarted; }
            set { SetProperty(ref _gameStarted, value); }
        }

        private Fret _activeFret { get; set; }

        private readonly INotesService _notesService;
        private readonly IEventAggregator _eventAggregator;
        private readonly Random _rnd;

        public MainModuleViewModel(INotesService notesService, IEventAggregator eventAggregator, ITimerService timer)
        {

            _notesService = notesService;
            _eventAggregator = eventAggregator;

            _eventAggregator.GetEvent<GameStartEvent>().Subscribe(OnGameStart);
            _eventAggregator.GetEvent<GameOverEvent>().Subscribe(OnGameOver);

            _rnd = new Random();

            ButtonClickedCommand = new DelegateCommand<string>(OnButtonClicked);
            KeyDownEventCommand = new DelegateCommand<KeyEventArgs>(OnKeyDown);

            FretsList = _notesService.GetFretboard() ?? new List<Fret>();
        }


        private void OnKeyDown(KeyEventArgs e)
        {
            var keys = new List<Key>
            {
            Key.A,
            Key.B,
            Key.C,
            Key.D,
            Key.E,
            Key.F
            };

            if (!(keys.Any(x => x == e.Key) && GameStarted))
            {
                e.Handled = true;
                return;
            }
            var key = keys.First(x => x == e.Key).ToString();

            if (Keyboard.IsKeyDown(Key.LeftShift))
            {
                OnButtonClicked($"{key}#");
            }
            else if (Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                switch (key)
                {
                    case "A":
                        OnButtonClicked($"{"G"}#");
                        break;
                    case "B":
                        OnButtonClicked($"{"A"}#");
                        break;
                    case "D":
                        OnButtonClicked($"{"C"}#");
                        break;
                    case "E":
                        OnButtonClicked($"{"D"}#");
                        break;
                    case "G":
                        OnButtonClicked($"{"F#"}#");
                        break;
                }
            }
            else
            {
                OnButtonClicked(key);
            }
        }

        private void OnButtonClicked(string buttonName)
        {
            if (buttonName == _activeFret?.Note)
            {
                _eventAggregator.GetEvent<AnsweredEvent>().Publish(true);
                PickRandomFret();
            }
            else
            {
                _eventAggregator.GetEvent<AnsweredEvent>().Publish(false);
                PickRandomFret();
            }
        }

        private void OnGameOver(HighScore highScore = null)
        {
            GameStarted = false;
            _activeFret.IsVisible = Visibility.Hidden;
            KeyDownEventCommand = null;
        }

        private void OnGameStart()
        {
            GameStarted = true;
            PickRandomFret();
        }

        private void PickRandomFret()
        {
            if (_activeFret != null)
            {
                _activeFret.IsVisible = Visibility.Hidden;
            }

            _activeFret = FretsList[_rnd.Next(FretsList.Count)];
            _activeFret.IsVisible = Visibility.Visible;
        }
    }
}
