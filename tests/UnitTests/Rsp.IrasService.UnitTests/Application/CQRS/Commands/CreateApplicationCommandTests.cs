namespace Rsp.IrasService.UnitTests.Application.CQRS.Commands;

public class CreateApplicationCommandTests
{
    private readonly Mock<IMediator> _mediatorMock;

    public CreateApplicationCommandTests()
    {
        _mediatorMock = new Mock<IMediator>();
    }

    [Fact]
    public async Task Send_CreateApplicationCommand_ShouldReturnExpectedResponse()
    {
        // Arrange
        var request = new ApplicationRequest
        {
            ApplicationId = "Test-123",
            Status = "Success"
        };

        var expectedResponse = new ApplicationResponse
        {
            ApplicationId = "Test-123",
            Status = "Success"
        };

        var command = new CreateApplicationCommand(request);

        _mediatorMock
            .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _mediatorMock.Object.Send(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedResponse.ApplicationId, result.ApplicationId);
        Assert.Equal(expectedResponse.Status, result.Status);

        _mediatorMock.Verify(m => m.Send(command, It.IsAny<CancellationToken>()), Times.Once);
    }
}