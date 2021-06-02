using GuitarNoteTeacher.Core.Services.NotesService;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace GuitarNoteTeacher.UnitTests
{
    public class NotesServiceTests
    {

        [Fact]
        public void ShouldReturnFretboard()
        {
            //Arrange
            var instance = Activator.CreateInstance<NotesService>();
            var fretList = new List<Fret>();

            //Act
            fretList.AddRange(instance.GetFretboard());

            //Assert
            Assert.NotEmpty(fretList);
            Assert.Equal(138, fretList.Count);
        }

        [Fact]
        public void ShouldNotReturnNullsAndEmptyStrings()
        {
            //Arrange
            var instance = Activator.CreateInstance<NotesService>();
            var fretList = new List<Fret>();

            //Act
            fretList.AddRange(instance.GetFretboard());

            //Assert
            Assert.True(fretList.TrueForAll(x => !string.IsNullOrEmpty(x.Name) && !string.IsNullOrEmpty(x.Note)));
        }

        [Theory]
        [InlineData("e14","F#")]
        [InlineData("A18", "D#")]
        [InlineData("G2", "A")]
        public void ShouldHaveTheCorrectNode(string value, string expected)
        {
            //Arrange
            var instance = Activator.CreateInstance<NotesService>();
            var fretList = new List<Fret>();

            //Act
            fretList.AddRange(instance.GetFretboard());
            var testNotes = fretList.Where(x => x.Name == value).ToList();
            var testNote = testNotes.FirstOrDefault(x => x.Name == value);

            //Assert
            Assert.Equal(expected, testNote?.Note);
        }
    }
}
