using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Application.DTOS.Responses;

namespace Rsp.Service.UnitTests.Application.CQRS.Commands;

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
            Id = "Test-123",
            Status = "Success"
        };

        var expectedResponse = new ApplicationResponse
        {
            Id = "Test-123",
            Status = "Success"
        };

        var command = new CreateApplicationCommand(request);

        _mediatorMock
            .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _mediatorMock.Object.Send(command, CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();
        result.Id.ShouldBe(expectedResponse.Id);
        result.Status.ShouldBe(expectedResponse.Status);

        _mediatorMock.Verify(m => m.Send(command, It.IsAny<CancellationToken>()), Times.Once);
    }
}