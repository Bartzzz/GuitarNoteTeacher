using GuitarNoteTeacher.Core.Events;
using GuitarNoteTeacher.Core.Services.PointsService.Objects;
using GuitarNoteTeacher.UI.Dialogs;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace GuitarNoteTeacher.UI.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Guitar Note Teacher";
        private readonly IEventAggregator _eventAggregator;
        private readonly IDialogService _dialogService;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel(IEventAggregator eventAggregator, IDialogService dialogService)
        {
            _eventAggregator = eventAggregator;
            _dialogService = dialogService;
            _eventAggregator.GetEvent<GameOverEvent>().Subscribe(OnGameOverEventHandler);
        }

        private void OnGameOverEventHandler(HighScore lastGameScore)
        {
            var p = new DialogParameters();
            p.Add("lastScore", lastGameScore);
            _dialogService.ShowDialog(nameof(GameOverDialog), p, r => { });
        }
    }
}
