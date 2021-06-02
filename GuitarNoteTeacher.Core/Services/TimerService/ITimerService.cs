using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace GuitarNoteTeacher.Core.Services.TimerService
{
    public interface ITimerService
    {
        int GameTime { get; }
        int AddTimeBonus(int Time);
        void StartTimer();
        void StopTimer();   
    }
}
