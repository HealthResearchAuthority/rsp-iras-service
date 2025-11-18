using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Domain.Entities;
using Rsp.IrasService.Services;

namespace Rsp.IrasService.UnitTests.Services.ProjectModificationServiceTests;

/// <summary>
///     Covers the tests for GetDocumentsForModification method
/// </summary>
public class GetDocumentsForModificationTests : TestServiceBase<ProjectModificationService>
{
    [Theory, AutoData]
    public async Task Returns_Paged_Result_With_TotalCount_And_ProjectId(
        Guid projectRecordId,
        ProjectOverviewDocumentSearchRequest search,
        int pageNumber,
        int pageSize,
        string sortField,
        string sortDirection,
        List<ProjectOverviewDocumentResult> documents)
    {
        // Arrange
        var repo = new Mock<IProjectModificationRepository>();
        repo.Setup(r => r.GetDocumentsForModification(search, pageNumber, pageSize, sortField, sortDirection, projectRecordId))
            .Returns(documents);
        repo.Setup(r => r.GetDocumentsForModificationCount(search, projectRecordId))
            .Returns(documents.Count);

        Mocker.Use(repo.Object);
        Sut = Mocker.CreateInstance<ProjectModificationService>();

        // Act
        var result = await Sut.GetDocumentsForModification(projectRecordId, search, pageNumber, pageSize, sortField, sortDirection);

        // Assert
        result.ShouldNotBeNull();
        result.TotalCount.ShouldBe(documents.Count);
        result.Documents.Count().ShouldBe(documents.Count);
    }
}