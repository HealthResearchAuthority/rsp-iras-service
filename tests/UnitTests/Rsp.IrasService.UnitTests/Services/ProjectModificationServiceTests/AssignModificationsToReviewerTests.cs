using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Services;

namespace Rsp.IrasService.UnitTests.Services.ProjectModificationServiceTests;

/// <summary>
///     Covers the tests for AssignModificationsToReviewer method
/// </summary>
public class AssignModificationsToReviewerTests : TestServiceBase<ProjectModificationService>
{
    [Theory, AutoData]
    public async Task Calls_Repository_With_Passed_Arguments(List<string> modificationIds, string reviewerId)
    {
        // Arrange
        var repo = new Mock<IProjectModificationRepository>();
        repo.Setup(r => r.AssignModificationsToReviewer(modificationIds, reviewerId))
            .Returns(Task.CompletedTask)
            .Verifiable();

        Mocker.Use(repo.Object);
        Sut = Mocker.CreateInstance<ProjectModificationService>();

        // Act
        await Sut.AssignModificationsToReviewer(modificationIds, reviewerId);

        // Assert
        repo.Verify(r => r.AssignModificationsToReviewer(modificationIds, reviewerId), Times.Once);
    }
}
