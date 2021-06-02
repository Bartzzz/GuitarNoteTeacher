using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuitarNoteTeacher.Core.Services.NotesService
{
    public interface INotesService
    {
       List<Fret> GetFretboard();
    }
}
