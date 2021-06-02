using GuitarNoteTeacher.Core.Services.PointsService;
using GuitarNoteTeacher.Core.Services.PointsService.Objects;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Windows;

namespace GuitarNoteTeacher.UI.Dialogs
{
    public class GameOverDialogViewModel : BindableBase, IDialogAware
    {
        public string Title => "Game Over";
        private IPointsService _pointsService;

        private string _initials;
        public string Initials
        {
            get { return _initials; }
            set { SetProperty(ref _initials, value); }
        }

        private Visibility _isHighScore;
        public Visibility IsHighScore
        {
            get{ return _isHighScore; }
            set { SetProperty(ref _isHighScore, value); }
        }

        private HighScore _lastGameScore;
        public HighScore LastGameScore
        {
            get { return _lastGameScore; }
            set { SetProperty(ref _lastGameScore, value); }
        }

        public GameOverDialogViewModel(IPointsService pointsService)
        {
            CloseDialogCommand = new DelegateCommand(CloseDialog);
            _pointsService = pointsService;
        }

        public DelegateCommand CloseDialogCommand { get; }

        public event Action<IDialogResult> RequestClose;

        private void CloseDialog()
        {
            var buttonResult = ButtonResult.OK;

            RequestClose?.Invoke(new DialogResult(buttonResult, new DialogParameters()));
        }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            if(LastGameScore.IsHighScore == true)
            {
                _pointsService.SetNewHighScore(new HighScore { Name = Initials, Points = LastGameScore.Points });
            }   
        }

        public void OnDialogOpened(IDialogParameters parameters)
        { 
            LastGameScore = parameters.GetValue<HighScore>("lastScore");
            IsHighScore = LastGameScore?.IsHighScore == true ? Visibility.Visible : Visibility.Hidden;
        }
    }
}
