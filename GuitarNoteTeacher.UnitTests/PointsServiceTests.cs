using GuitarNoteTeacher.Core.Services.PointsService;
using System;
using System.IO;
using System.Linq;
using Xunit;

namespace GuitarNoteTeacher.UnitTests
{
    public class PointsServiceTests
    {
        [Fact]
        private void ShouldCreateHighScoreFileIfItDoesntExists()
        {
            //Arrange
            var instance = Activator.CreateInstance<PointsService>();
            //Assert
            Assert.True(File.Exists(instance._filePath));
        }

        [Fact]
        private void ShouldSaveNewHighScoreToNewFile()
        {
            //Arrange
            var instance = Activator.CreateInstance<PointsService>();
            instance._filePath = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}/HighScore.txt"; 

            //Act
            instance.SetNewHighScore(new Core.Services.PointsService.Objects.HighScore { Name = "ABC", Points = 123 });

            if (!File.Exists(instance._filePath))
            {
                return;
            }

            var file = File.ReadAllLines(instance._filePath);

            //Assert
            Assert.NotEmpty(file);
            Assert.True(file.Any(x => x == "ABC") && file.Any(x => x == "123"));
        }

        [Fact]
        private void ShouldLoadCorrectHighScoreFromFile()
        {
            //Arrange
            var instance = Activator.CreateInstance<PointsService>();
            instance._filePath = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}/HighScore.txt";

            //Act
            instance.SetNewHighScore(new Core.Services.PointsService.Objects.HighScore { Name = "ABC", Points = 123 });

            if (!File.Exists(instance._filePath))
            {
                return;
            }

            var highScore = instance.GetCurrentHighScore();

            //Assert
            Assert.NotNull(highScore);
            Assert.True(highScore.Name == "ABC" && highScore.Points == 123);
        }

        [Theory]
        [InlineData(1,1)]
        [InlineData(3,2)]
        [InlineData(8,3)]
        private void ShouldCalculatePointsCorrectly(int value1, int expected)
        {
            //Arrange
            var instance = Activator.CreateInstance<PointsService>();

            //Act
            var points = instance.AddPoints(value1);

            //Assert
            Assert.Equal(points, expected);
        }
    }
}
