using GuitarNoteTeacher.Core.Services.NotesService.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GuitarNoteTeacher.Core.Services.NotesService
{
    public class NotesService : INotesService
    {
        private string[] ZeroNotes = { "E", "A", "D", "G", "B", "e" };
        public List<Fret> GetFretboard()
        {
            var fretList = new List<Fret>();
            foreach (var note in ZeroNotes)
            {
                for (int i = 0; i <= 22; i++)
                {
                    var noteNumber = i > 12 ? i - 12 : i;
                    var GuitarFret = new Fret()
                    {
                        Name = $"{note}{i}",
                        Note = Notes.fretNotes.Where(x => x.Key == $"{note}{noteNumber}")?.FirstOrDefault().Value,
                        IsVisible = Visibility.Hidden
                    };

                    fretList.Add(GuitarFret);
                }
            }

            return fretList;

        }
    }
}

