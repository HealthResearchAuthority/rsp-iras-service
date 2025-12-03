using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Domain.Constants;
using Rsp.IrasService.Domain.Entities;
using Rsp.IrasService.Services;

namespace Rsp.IrasService.UnitTests.Services.ProjectModificationServiceTests;

public class GetModificationAuditTrailTests : TestServiceBase<ProjectModificationService>
{
    [Theory, AutoData]
    public async Task Returns_Complete_List_Of_ModificationAuditTrail_When_User_Is_Backstage_User
    (
        Guid projectModificationId,
        ClaimsPrincipal user
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

        var claimsIdentity = new ClaimsIdentity();
        claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, Roles.SystemAdministrator));

        user.AddIdentity(claimsIdentity);

        var httpContextAccessor = Mocker.GetMock<IHttpContextAccessor>();

        httpContextAccessor
            .Setup(h => h.HttpContext)
            .Returns(new DefaultHttpContext()
            {
                User = user
            });

        Mocker.Use(repo.Object);
        Sut = Mocker.CreateInstance<ProjectModificationService>();

        // Act
        var result = await Sut.GetModificationAuditTrail(projectModificationId);

        // Assert
        result.ShouldNotBeNull();
        result.Items.Count().ShouldBe(auditTrails.Count());
    }

    [Theory, AutoData]
    public async Task Returns_Filtered_List_Of_ModificationAuditTrail_When_User_Is_Frontstage_User
    (
        Guid projectModificationId,
        ClaimsPrincipal user
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

        var claimsIdentity = new ClaimsIdentity();
        claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, Roles.Applicant));

        user.AddIdentity(claimsIdentity);

        var httpContextAccessor = Mocker.GetMock<IHttpContextAccessor>();

        httpContextAccessor
            .Setup(h => h.HttpContext)
            .Returns(new DefaultHttpContext()
            {
                User = user
            });

        Mocker.Use(repo.Object);
        Sut = Mocker.CreateInstance<ProjectModificationService>();

        // Act
        var result = await Sut.GetModificationAuditTrail(projectModificationId);

        // Assert
        result.ShouldNotBeNull();
        result.Items.Count().ShouldBe(auditTrails.Count(at => !at.IsBackstageOnly));
    }
}