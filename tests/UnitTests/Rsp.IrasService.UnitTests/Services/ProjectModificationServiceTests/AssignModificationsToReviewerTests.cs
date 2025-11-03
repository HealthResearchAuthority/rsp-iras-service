using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Services;

namespace Rsp.IrasService.UnitTests.Services.ProjectModificationServiceTests;

/// <summary>
///     Covers the tests for AssignModificationsToReviewer method
/// </summary>
public class AssignModificationsToReviewerTests : TestServiceBase<ProjectModificationService>
{
    [Theory, AutoData]
    public async Task Calls_Repository_With_Passed_Arguments(List<string> modificationIds, string reviewerId, string reviewerEmail)
    {
        // Arrange
        var repo = new Mock<IProjectModificationRepository>();
        repo.Setup(r => r.AssignModificationsToReviewer(modificationIds, reviewerId, reviewerEmail))
            .Returns(Task.CompletedTask)
            .Verifiable();

        Mocker.Use(repo.Object);
        Sut = Mocker.CreateInstance<ProjectModificationService>();

        // Act
        await Sut.AssignModificationsToReviewer(modificationIds, reviewerId, reviewerEmail);

        // Assert
        repo.Verify(r => r.AssignModificationsToReviewer(modificationIds, reviewerId, reviewerEmail), Times.Once);
    }
}