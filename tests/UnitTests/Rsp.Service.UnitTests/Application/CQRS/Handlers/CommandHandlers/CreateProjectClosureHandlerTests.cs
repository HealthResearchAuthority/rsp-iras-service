using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.Application.CQRS.Handlers.CommandHandlers;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Application.DTOS.Responses;

namespace Rsp.Service.UnitTests.Application.CQRS.Handlers.CommandHandlers;

public class CreateProjectClosureHandlerTests
{
    private readonly Mock<IProjectClosureService> _projectClosureServiceMock;
    private readonly CreateProjectClosureHandler _handler;

    public CreateProjectClosureHandlerTests()
    {
        _projectClosureServiceMock = new Mock<IProjectClosureService>();
        _handler = new CreateProjectClosureHandler(_projectClosureServiceMock.Object);
    }

    [Fact]
    public async Task Handle_CreateProjectClosure_Command_ShouldReturnExpectedResponse()
    {
        // Arrange
        var request = new ProjectClosureRequest
        {
            Id = Guid.NewGuid(),
            ProjectRecordId = "123",
            ShortProjectTitle = "New Project",
            Status = "with sponsor",
            ClosureDate = DateTime.Now,
            SentToSponsorDate = DateTime.Now,
            DateActioned = DateTime.Now,
            UserId = "123"
        };

        var expectedResponse = new ProjectClosureResponse
        {
            Id = Guid.NewGuid(),
            ProjectRecordId = "123",
            Status = "with sponsor",
        };

        var command = new CreateProjectClosureCommand(request);

        _projectClosureServiceMock
            .Setup(service => service.CreateProjectClosure(request))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();
        result.Id.ShouldBe(expectedResponse.Id);
        result.Status.ShouldBe(expectedResponse.Status);

        _projectClosureServiceMock.Verify(service => service.CreateProjectClosure(request), Times.Once);
    }
}