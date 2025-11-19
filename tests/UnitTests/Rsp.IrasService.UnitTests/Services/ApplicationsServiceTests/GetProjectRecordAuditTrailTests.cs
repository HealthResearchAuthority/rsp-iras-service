using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Domain.Entities;
using Rsp.IrasService.Services;
using Rsp.IrasService.UnitTests.Fixtures;

namespace Rsp.IrasService.UnitTests.Services.ApplicationsServiceTests;

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