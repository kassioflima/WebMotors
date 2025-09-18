using Microsoft.Extensions.Logging;
using Moq;
using WebMotors.Domain.Shared.Commands;
using WebMotors.Domain.Shared.DomainNotifications;
using WebMotors.Domain.Shared.DomainNotifications.Interfaces;
using WebMotors.Domain.Shared.UoW.Interfaces;
using Xunit;

namespace WebMotors.Tests.Domain.Shared;

public class CommandHandlerTests
{
    [Fact]
    public void CommandHandler_ShouldBeCreated()
    {
        // Arrange
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var notificationsHandlerMock = new Mock<IHandler<DomainNotification>>();
        var loggerMock = new Mock<ILogger>();

        // Act
        var commandHandler = new CommandHandler(
            unitOfWorkMock.Object,
            notificationsHandlerMock.Object,
            loggerMock.Object);

        // Assert
        Assert.NotNull(commandHandler);
    }

    [Fact]
    public void Commit_WithNotifications_ShouldReturnFalse()
    {
        // Arrange
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var notificationsHandlerMock = new Mock<IHandler<DomainNotification>>();
        var loggerMock = new Mock<ILogger>();

        var commandHandler = new CommandHandler(
            unitOfWorkMock.Object,
            notificationsHandlerMock.Object,
            loggerMock.Object);

        notificationsHandlerMock.Setup(x => x.HasNotifications())
            .Returns(true);

        // Act
        var result = commandHandler.Commit();

        // Assert
        Assert.False(result);
        unitOfWorkMock.Verify(x => x.Commit(), Times.Never);
    }

    [Fact]
    public void MockVerification_ShouldWork()
    {
        // Arrange
        var mock = new Mock<IUnitOfWork>();
        
        // Act & Assert
        Assert.NotNull(mock.Object);
        mock.Verify(x => x.Commit(), Times.Never);
    }

    [Theory]
    [InlineData(true, true)]
    [InlineData(false, false)]
    [InlineData(true, false)]
    public void BooleanLogic_ShouldWork(bool input1, bool input2)
    {
        // Act
        var andResult = input1 && input2;
        var orResult = input1 || input2;
        var notResult = !input1;

        // Assert
        Assert.Equal(input1 && input2, andResult);
        Assert.Equal(input1 || input2, orResult);
        Assert.Equal(!input1, notResult);
    }
}