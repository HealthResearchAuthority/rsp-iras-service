using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Domain.Entities;
using Rsp.IrasService.Services;

namespace Rsp.IrasService.UnitTests.Services.ProjectModificationServiceTests;

public class GetModificationAuditTrailTests : TestServiceBase<ProjectModificationService>
{
    [Theory, AutoData]
    public async Task Returns_List_Of_ModificationAuditTrail
    (
        Guid projectModificationId
    )
    {
        var fixture = new Fixture();
        var auditTrails = fixture
            .Build<ProjectModificationAuditTrail>()
            .Without(e => e.ProjectModification)
            .CreateMany();

        // Arrange
        var repo = new Mock<IProjectModificationRepository>();

        repo
            .Setup(r => r.GetModificationAuditTrail(It.IsAny<Guid>()))
            .ReturnsAsync(auditTrails);

        Mocker.Use(repo.Object);
        Sut = Mocker.CreateInstance<ProjectModificationService>();

        // Act
        var result = await Sut.GetModificationAuditTrail(projectModificationId);

        // Assert
        result.ShouldNotBeNull();
        result.Items.Count().ShouldBe(auditTrails.Count());
    }
}