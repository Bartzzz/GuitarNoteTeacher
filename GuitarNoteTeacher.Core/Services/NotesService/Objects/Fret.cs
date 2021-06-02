using System.Windows;
using Prism.Mvvm;

namespace GuitarNoteTeacher.Core.Services.NotesService
{
    public class Fret: BindableBase
    {
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set 
            {
                SetProperty(ref _name, value);
            }
        }
        private string _note;
        public string Note {
            get
            {
                return _note;
            }
            set 
            {
                SetProperty(ref _note, value);
            } 
        }
        private Visibility _isVisible;
        public Visibility IsVisible
        {
            get
            {
                return _isVisible;
            }
            set
            {
                SetProperty(ref _isVisible, value);
            }
        }
    }

}
