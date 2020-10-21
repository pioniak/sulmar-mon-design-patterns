using FluentAssertions;
using Moq;
using StatePattern;
using System;
using Xunit;

namespace StatePatternUnitTests
{
    public class LampTests
    {
        private Mock<IMessageService> mockMessageService;
        private IMessageService messageService;


        public LampTests()
        {
            mockMessageService = new Mock<IMessageService>();
            messageService = mockMessageService.Object;
        }


        [Theory]
        [InlineData("12:59", LampState.Off)]
        [InlineData("13:00", LampState.On)]
        public void PushUp_LampStateOff_ShouldTargetState(string currentTime, LampState expectedState)
        {
            ITimeService timeService = Mock.Of<ITimeService>(ts=>ts.TimeOfDay==TimeSpan.Parse(currentTime));

            // Arrange
            ProxyLamp lamp = new ProxyLamp(new LampStateMachine(messageService, timeService));

            // Act
            lamp.PushUp();

            // Assert
            lamp.State.Should().Be(expectedState);

        }

        [Theory]
        [InlineData(LampState.On, LampState.Red)]
        [InlineData(LampState.Red, LampState.On)]
        public void PushUp_CurrentState_ShouldTargetState(LampState currentState, LampState expectedState)
        {
            // Arrange
            ProxyLamp lamp = new ProxyLamp(new LampStateMachine(messageService, initialState: currentState));

            // Act
            lamp.PushUp();

            // Assert
            lamp.State.Should().Be(expectedState);
        }

        [Theory]
        [InlineData(LampState.Off, LampState.Off)]
        [InlineData(LampState.On, LampState.Off)]
        [InlineData(LampState.Red, LampState.Off)]
        public void PushDown_CurrentState_ShouldTargetState(LampState currentState, LampState expectedState)
        {
            // Arrange
            ProxyLamp lamp = new ProxyLamp(new LampStateMachine(messageService, initialState: currentState));

            // Act
            lamp.PushDown();

            // Assert
            lamp.State.Should().Be(expectedState);
        }

        [Fact]
        public void PushUp_LampStateOff_ShouldSendMessage()
        {

        }



        [Fact]
        public void PushUp_LampStateOff_ShouldSend()
        {

        }
    }
}
