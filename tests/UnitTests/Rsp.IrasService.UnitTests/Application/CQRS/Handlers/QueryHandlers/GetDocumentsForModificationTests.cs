using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Handlers.QueryHandlers;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.IrasService.Domain.Entities;
using Rsp.IrasService.Services;

namespace Rsp.IrasService.UnitTests.Application.CQRS.Handlers.QueryHandlers;

public class GetDocumentsForModificationTests
{
    private readonly Mock<IProjectModificationService> _modificationServiceMock;
    private readonly GetDocumentsForModificationHandler _handler;

    public GetDocumentsForModificationTests()
    {
        _modificationServiceMock = new Mock<IProjectModificationService>();
        _handler = new GetDocumentsForModificationHandler(_modificationServiceMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnExpectedDocumentResponse()
    {
        // Arrange
        Guid modificationId = Guid.NewGuid();
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
            TotalCount = expectedModifications.Count
        };

        var query = new GetDocumentsForModificationQuery(
            modificationId,
            searchQuery,
            pageNumber: 1,
            pageSize: 10,
            sortField: "CreatedAt",
            sortDirection: "asc"
        );

        _modificationServiceMock
            .Setup(service => service.GetDocumentsForModification(
                query.ModificationId,
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
        result.Documents.ShouldHaveSingleItem();
        result.Documents.First().FileName.ShouldBe("MOD-001");

        _modificationServiceMock.Verify(service =>
            service.GetDocumentsForModification(
                query.ModificationId,
                query.SearchQuery,
                query.PageNumber,
                query.PageSize,
                query.SortField,
                query.SortDirection),
            Times.Once);
    }

    [Theory]
    [AutoData]
    public async Task GetDocumentsForModification_ShouldReturnMappedResponseWithProjectId(
    Guid projectRecordId,
    ProjectOverviewDocumentSearchRequest searchRequest,
    List<ProjectOverviewDocumentResult> domainDocuments,
    int pageNumber,
    int pageSize,
    string sortField,
    string sortDirection)
    {
        // Arrange
        var mockRepo = new Mock<IProjectModificationRepository>();
        mockRepo.Setup(r => r.GetDocumentsForModification(searchRequest, pageNumber, pageSize, sortField, sortDirection, projectRecordId))
                .Returns(domainDocuments);

        mockRepo.Setup(r => r.GetDocumentsForModificationCount(searchRequest, projectRecordId))
                .Returns(domainDocuments.Count);

        var service = new ProjectModificationService(mockRepo.Object);

        // Act
        var result = await service.GetDocumentsForModification(projectRecordId, searchRequest, pageNumber, pageSize, sortField, sortDirection);

        // Assert
        result.ShouldNotBeNull();
        result.Documents.Count().ShouldBe(domainDocuments.Count);
        result.TotalCount.ShouldBe(domainDocuments.Count);
    }
}