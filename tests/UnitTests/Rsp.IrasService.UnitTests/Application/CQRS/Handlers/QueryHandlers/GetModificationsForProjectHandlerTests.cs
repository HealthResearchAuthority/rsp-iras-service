using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Handlers.QueryHandlers;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.UnitTests.Application.CQRS.Handlers.QueryHandlers;

public class GetModificationsForProjectHandlerTests
{
    private readonly Mock<IProjectModificationService> _modificationServiceMock;
    private readonly GetModificationsForProjectHandler _handler;

    public GetModificationsForProjectHandlerTests()
    {
        _modificationServiceMock = new Mock<IProjectModificationService>();
        _handler = new GetModificationsForProjectHandler(_modificationServiceMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnExpectedModificationResponse()
    {
        // Arrange
        var projectRecordId = "PR-001";
        var searchQuery = new ModificationSearchRequest
        {
            IrasId = "IRAS-123",
            ChiefInvestigatorName = "Dr. Smith"
        };

        var expectedModifications = new List<ModificationDto>
        {
            new() { ModificationId = "MOD-001", ChiefInvestigator = "Dr. Smith" }
        };

        var expectedResponse = new ModificationResponse
        {
            Modifications = expectedModifications,
            TotalCount = expectedModifications.Count,
            ProjectRecordId = projectRecordId
        };

        var query = new GetModificationsForProjectQuery(
            projectRecordId,
            searchQuery,
            pageNumber: 1,
            pageSize: 10,
            sortField: "CreatedAt",
            sortDirection: "asc"
        );

        _modificationServiceMock
            .Setup(service => service.GetModificationsForProject(
                query.ProjectRecordId,
                query.SearchQuery,
                query.PageNumber,
                query.PageSize,
                query.SortField,
                query.SortDirection))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();
        result.TotalCount.ShouldBe(1);
        result.ProjectRecordId.ShouldBe(projectRecordId);
        result.Modifications.ShouldHaveSingleItem();
        result.Modifications.First().ModificationId.ShouldBe("MOD-001");

        _modificationServiceMock.Verify(service =>
            service.GetModificationsForProject(
                query.ProjectRecordId,
                query.SearchQuery,
                query.PageNumber,
                query.PageSize,
                query.SortField,
                query.SortDirection),
            Times.Once);
    }
}