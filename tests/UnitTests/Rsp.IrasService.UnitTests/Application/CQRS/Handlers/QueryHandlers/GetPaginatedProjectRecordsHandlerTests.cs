using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Handlers.QueryHandlers;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.UnitTests.Application.CQRS.Handlers.QueryHandlers;

public class GetPaginatedProjectRecordsHandlerTests
{
    private readonly Mock<IApplicationsService> _applicationsServiceMock;
    private readonly GetPaginatedProjectRecordsHandler _handler;

    public GetPaginatedProjectRecordsHandlerTests()
    {
        _applicationsServiceMock = new Mock<IApplicationsService>();
        _handler = new GetPaginatedProjectRecordsHandler(_applicationsServiceMock.Object);
    }

    [Fact]
    public async Task Handle_GetPaginatedApplicationsQuery_ShouldReturnPaginatedApplications()
    {
        // Arrange
        var pageIndex = 1;
        var pageSize = 10;
        var expectedItems = new List<CompleteProjectRecordResponse>
        {
            new() { Id = "App-123", Status = "Approved" },
            new() { Id = "App-456", Status = "Pending" }
        };

        var expectedResponse = new PaginatedResponse<CompleteProjectRecordResponse>
        {
            Items = expectedItems,
            TotalCount = 2
        };

        var query = new GetPaginatedProjectRecordsQuery
        {
            PageIndex = pageIndex,
            PageSize = pageSize,
            SearchQuery = null
        };

        _applicationsServiceMock
            .Setup(service => service.GetPaginatedApplications(null, pageIndex, pageSize, null, null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();
        result.TotalCount.ShouldBe(2);

        _applicationsServiceMock.Verify(service =>
            service.GetPaginatedApplications(null, pageIndex, pageSize, null, null), Times.Once);
    }
}