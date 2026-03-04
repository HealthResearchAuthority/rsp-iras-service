using Rsp.Service.Application.Contracts.Repositories;
using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.Service.UnitTests.Services.ProjectModificationServiceTests;

/// <summary>
///     Covers the tests for GetDocumentsForProjectOverview method
/// </summary>
public class GetProjectDocumentsAuditTrailTests : TestServiceBase<Service.Services.DocumentService>
{
    [Theory, AutoData]
    public async Task Returns_Paged_Result_With_TotalCount_And_ProjectId(
        string projectRecordId,
        int pageNumber,
        int pageSize,
        string sortField,
        string sortDirection,
        IEnumerable<ModificationDocumentsAuditTrailDto> documentsAudit)
    {
        // Arrange
        var repo = new Mock<IDocumentRepository>();
        repo.Setup(r => r.GetProjectDocumentsAuditTrail(pageNumber, pageSize, sortField, sortDirection, projectRecordId, false))
            .ReturnsAsync(documentsAudit);

        Mocker.Use(repo.Object);
        Sut = Mocker.CreateInstance<Service.Services.DocumentService>();

        // Act
        var result = await Sut.GetProjectDocumentsAuditTrail(projectRecordId, pageNumber, pageSize, sortField, sortDirection);

        // Assert
        result.ShouldNotBeNull();
        result.Items.Count().ShouldBe(documentsAudit.Count());
    }
}