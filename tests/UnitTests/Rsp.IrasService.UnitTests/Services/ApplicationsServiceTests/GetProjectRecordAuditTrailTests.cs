using Rsp.Service.Application.Contracts.Repositories;
using Rsp.Service.Domain.Entities;
using Rsp.Service.Services;
using Rsp.Service.UnitTests.Fixtures;

namespace Rsp.Service.UnitTests.Services.ApplicationsServiceTests;

public class GetProjectRecordAuditTrailTests : TestServiceBase<ApplicationsService>
{
    [Theory, NoRecursionAutoData]
    public async Task Returns_List_Of_AuditTrail
    (
        string projectRecordId,
        IEnumerable<ProjectRecordAuditTrail> auditTrails
    )
    {
        // Arrange
        var repo = new Mock<IProjectRecordRepository>();

        repo.Setup(r => r.GetProjectRecordAuditTrail(It.IsAny<string>()))
            .ReturnsAsync(auditTrails);

        Mocker.Use(repo.Object);
        Sut = Mocker.CreateInstance<ApplicationsService>();

        // Act
        var result = await Sut.GetProjectRecordAuditTrail(projectRecordId);

        // Assert
        result.ShouldNotBeNull();
        result.Items.Count().ShouldBe(auditTrails.Count());
    }
}