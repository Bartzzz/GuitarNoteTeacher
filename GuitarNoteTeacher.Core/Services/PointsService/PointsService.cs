using GuitarNoteTeacher.Core.Services.PointsService.Objects;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace GuitarNoteTeacher.Core.Services.PointsService
{
    public class PointsService : IPointsService
    {
        public string _filePath = String.Concat(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "/HighScore.txt");

        public PointsService()
        {
            if(!File.Exists(_filePath))
            {
                File.Create(_filePath);
            }
        }

        public int AddPoints(int goodAnswersStreak)
        {
            if (goodAnswersStreak == 3)
            {
                return 2;
            }
            else if (goodAnswersStreak > 3)
            {
                return 3;
            }
            else
            {
                return 1;
            }
        }

        public HighScore GetCurrentHighScore()
        {
            var score = File.ReadAllLines(_filePath).ToList();
            var highScore = new HighScore();
            if(score.Any())
            {
                highScore.Name = score[0];
                int.TryParse(score[1], out var points);
                highScore.Points = points;
            }

            return highScore;
        }

        public void SetNewHighScore(HighScore highScore)
        {
            var score = new string[] { highScore.Name, highScore.Points.ToString()};
            File.WriteAllLines(_filePath, score);
        }
    }
}
