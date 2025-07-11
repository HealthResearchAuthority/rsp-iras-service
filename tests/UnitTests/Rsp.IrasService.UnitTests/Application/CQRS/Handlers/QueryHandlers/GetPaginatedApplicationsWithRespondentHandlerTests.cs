using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Handlers.QueryHandlers;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.UnitTests.Application.CQRS.Handlers.QueryHandlers;

public class GetPaginatedApplicationsWithRespondentHandlerTests
{
    private readonly Mock<IApplicationsService> _applicationsServiceMock;
    private readonly GetPaginatedApplicationsWithRespondentHandler _handler;

    public GetPaginatedApplicationsWithRespondentHandlerTests()
    {
        _applicationsServiceMock = new Mock<IApplicationsService>();
        _handler = new GetPaginatedApplicationsWithRespondentHandler(_applicationsServiceMock.Object);
    }

    [Fact]
    public async Task Handle_GetPaginatedApplicationsWithRespondentQuery_ShouldReturnPaginatedApplications()
    {
        // Arrange
        var respondentId = "R-123";
        var pageIndex = 0;
        var pageSize = 10;
        var expectedItems = new List<ApplicationResponse>
        {
            new() { Id = "App-123", Status = "Approved" },
            new() { Id = "App-456", Status = "Pending" }
        };

        var expectedResponse = new PaginatedResponse<ApplicationResponse>
        {
            Items = expectedItems,
            TotalCount = 2
        };

        var query = new GetPaginatedApplicationsWithRespondentQuery
        {
            RespondentId = respondentId,
            PageIndex = pageIndex,
            PageSize = pageSize,
            SearchQuery = null
        };

        _applicationsServiceMock
            .Setup(service => service.GetPaginatedRespondentApplications(respondentId, null, pageIndex, pageSize))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();
        result.TotalCount.ShouldBe(2);

        _applicationsServiceMock.Verify(service =>
            service.GetPaginatedRespondentApplications(respondentId, null, pageIndex, pageSize), Times.Once);
    }
}