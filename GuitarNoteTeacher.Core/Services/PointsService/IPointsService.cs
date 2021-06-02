using GuitarNoteTeacher.Core.Services.PointsService.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuitarNoteTeacher.Core.Services.PointsService
{
    public interface IPointsService
    {
       HighScore GetCurrentHighScore();
        void SetNewHighScore(HighScore highScore);
        int AddPoints(int goodAnswersStreak);
    }
}
