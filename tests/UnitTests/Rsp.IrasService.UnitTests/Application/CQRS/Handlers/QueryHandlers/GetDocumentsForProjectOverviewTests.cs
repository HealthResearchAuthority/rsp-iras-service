using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Handlers.QueryHandlers;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.UnitTests.Application.CQRS.Handlers.QueryHandlers;

public class GetDocumentsForProjectOverviewTests
{
    private readonly Mock<IProjectModificationService> _modificationServiceMock;
    private readonly GetDocumentsForProjectOverviewHandler _handler;

    public GetDocumentsForProjectOverviewTests()
    {
        _modificationServiceMock = new Mock<IProjectModificationService>();
        _handler = new GetDocumentsForProjectOverviewHandler(_modificationServiceMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnExpectedDocumentResponse()
    {
        // Arrange
        var projectRecordId = "PR-001";
        var searchQuery = new ProjectOverviewDocumentSearchRequest
        {
            IrasId = "IRAS-123"
        };

        var expectedModifications = new List<ProjectOverviewDocumentDto>
        {
            new() { FileName = "MOD-001", DocumentType = "Smith" }
        };

        var expectedResponse = new ProjectOverviewDocumentResponse
        {
            Documents = expectedModifications,
            TotalCount = expectedModifications.Count,
            ProjectRecordId = projectRecordId
        };

        var query = new GetDocumentsForProjectOverviewQuery(
            projectRecordId,
            searchQuery,
            pageNumber: 1,
            pageSize: 10,
            sortField: "CreatedAt",
            sortDirection: "asc"
        );

        _modificationServiceMock
            .Setup(service => service.GetDocumentsForProjectOverview(
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
        result.Documents.ShouldHaveSingleItem();
        result.Documents.First().FileName.ShouldBe("MOD-001");

        _modificationServiceMock.Verify(service =>
            service.GetDocumentsForProjectOverview(
                query.ProjectRecordId,
                query.SearchQuery,
                query.PageNumber,
                query.PageSize,
                query.SortField,
                query.SortDirection),
            Times.Once);
    }
}