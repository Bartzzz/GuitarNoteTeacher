using GuitarNoteTeacher.Core.Services.PointsService.Objects;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuitarNoteTeacher.Core.Events
{
    public class GameOverEvent : PubSubEvent<HighScore>
    {

    }
}
