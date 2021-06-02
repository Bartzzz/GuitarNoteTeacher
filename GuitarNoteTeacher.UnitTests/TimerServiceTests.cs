using GuitarNoteTeacher.Core.Services.TimerService;
using Prism.Events;
using Xunit;

namespace GuitarNoteTeacher.UnitTests
{
    public class TimerServiceTests
    {
        private readonly IEventAggregator _eventAggragator;
        private readonly TimerService _timerService;

        public TimerServiceTests()
        {
            _eventAggragator = new EventAggregator();
            _timerService = new TimerService(_eventAggragator);
        }
        [Fact]
        public void ShouldAddTimeBonusWhenInvoked()
        {
            //Arrange
            int time = 0;

            //Act
            time = _timerService.AddTimeBonus(time);

            //Assert
            Assert.Equal(10, time);
        }

        [Fact]
        public void ShouldBeLEqualToMaxTImeValue()
        {
            //Arrange
            var time = 105;
            var maxvalue = 110;

            //Act
            time = _timerService.AddTimeBonus(time);

            //Assert
            Assert.Equal(maxvalue, time);
        }

    }
}
