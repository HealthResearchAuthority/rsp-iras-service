using Rsp.Service.Application.Contracts.Repositories;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Domain.Entities;
using Rsp.Service.Services;

namespace Rsp.Service.UnitTests.Services.ProjectModificationServiceTests;

/// <summary>
///     Covers the tests for GetDocumentsForProjectOverview method
/// </summary>
public class GetDocumentsForProjectOverviewTests : TestServiceBase<ProjectModificationService>
{
    [Theory, AutoData]
    public async Task Returns_Paged_Result_With_TotalCount_And_ProjectId(
        string projectRecordId,
        ProjectOverviewDocumentSearchRequest search,
        int pageNumber,
        int pageSize,
        string sortField,
        string sortDirection,
        List<ProjectOverviewDocumentResult> documents)
    {
        // Arrange
        var repo = new Mock<IProjectModificationRepository>();
        repo.Setup(r => r.GetDocumentsForProjectOverview(search, pageNumber, pageSize, sortField, sortDirection, projectRecordId))
            .Returns(documents);
        repo.Setup(r => r.GetDocumentsForProjectOverviewCount(search, projectRecordId))
            .Returns(documents.Count);

        Mocker.Use(repo.Object);
        Sut = Mocker.CreateInstance<ProjectModificationService>();

        // Act
        var result = await Sut.GetDocumentsForProjectOverview(projectRecordId, search, pageNumber, pageSize, sortField, sortDirection);

        // Assert
        result.ShouldNotBeNull();
        result.TotalCount.ShouldBe(documents.Count);
        result.ProjectRecordId.ShouldBe(projectRecordId);
        result.Documents.Count().ShouldBe(documents.Count);
    }
}