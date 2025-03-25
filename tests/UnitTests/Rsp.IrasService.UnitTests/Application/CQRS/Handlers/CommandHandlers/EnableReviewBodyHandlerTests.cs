﻿using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.CQRS.Handlers.CommandHandlers;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.UnitTests.Application.CQRS.Handlers.CommandHandlers;

public class EnableReviewBodyHandlerTests
{
    private readonly EnableReviewBodyHandler _handler;
    private readonly Mock<IReviewBodyService> _reviewBodyServiceMock;

    public EnableReviewBodyHandlerTests()
    {
        _reviewBodyServiceMock = new Mock<IReviewBodyService>();
        _handler = new EnableReviewBodyHandler(_reviewBodyServiceMock.Object);
    }

    [Fact]
    public async Task Handle_GetApplicationsQuery_ShouldReturnListOfApplications()
    {
        // Arrange
        var guid = Guid.NewGuid();

        var expectedResponse = new ReviewBodyDto
        {
            Id = guid,
            OrganisationName = "App-123",
            Description = "Approved"
        };

        var query = new EnableReviewBodyCommand(guid);

        _reviewBodyServiceMock
            .Setup(service => service.EnableReviewBody(guid))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();

        _reviewBodyServiceMock.Verify(service => service.EnableReviewBody(It.IsAny<Guid>()), Times.Once);
    }
}