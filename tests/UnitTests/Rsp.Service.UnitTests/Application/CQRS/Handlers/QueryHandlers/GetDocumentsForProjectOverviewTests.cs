using Microsoft.AspNetCore.Http;
using Rsp.Service.Application.Contracts.Repositories;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Handlers.QueryHandlers;
using Rsp.Service.Application.CQRS.Queries;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Application.DTOS.Responses;
using Rsp.Service.Domain.Entities;
using Rsp.Service.Services;

namespace Rsp.Service.UnitTests.Application.CQRS.Handlers.QueryHandlers;

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

    [Theory]
    [AutoData]
    public async Task GetDocumentsForProjectOverview_ShouldReturnMappedResponseWithProjectId(
    string projectRecordId,
    ProjectOverviewDocumentSearchRequest searchRequest,
    List<ProjectOverviewDocumentResult> domainDocuments,
    int pageNumber,
    int pageSize,
    string sortField,
    string sortDirection)
    {
        // Arrange
        var mockRepo = new Mock<IProjectModificationRepository>();
        mockRepo.Setup(r => r.GetDocumentsForProjectOverview(searchRequest, pageNumber, pageSize, sortField, sortDirection, projectRecordId))
                .Returns(domainDocuments);

        mockRepo.Setup(r => r.GetDocumentsForProjectOverviewCount(searchRequest, projectRecordId))
                .Returns(domainDocuments.Count);

        var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

        var service = new ProjectModificationService(mockRepo.Object, mockHttpContextAccessor.Object);

        // Act
        var result = await service.GetDocumentsForProjectOverview(projectRecordId, searchRequest, pageNumber, pageSize, sortField, sortDirection);

        // Assert
        result.ShouldNotBeNull();
        result.Documents.Count().ShouldBe(domainDocuments.Count);
        result.TotalCount.ShouldBe(domainDocuments.Count);
        result.ProjectRecordId.ShouldBe(projectRecordId);
    }
}