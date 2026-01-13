using Rsp.Service.Application.Contracts.Repositories;
using Rsp.Service.Services;

namespace Rsp.Service.UnitTests.Services.ProjectModificationServiceTests;

/// <summary>
///     Covers the tests for AssignModificationsToReviewer method
/// </summary>
public class AssignModificationsToReviewerTests : TestServiceBase<ProjectModificationService>
{
    [Theory, AutoData]
    public async Task Calls_Repository_With_Passed_Arguments(List<string> modificationIds, string reviewerId, string reviewerEmail, string reviewerName)
    {
        // Arrange
        var repo = new Mock<IProjectModificationRepository>();
        repo.Setup(r => r.AssignModificationsToReviewer(modificationIds, reviewerId, reviewerEmail,reviewerName))
            .Returns(Task.CompletedTask)
            .Verifiable();

        Mocker.Use(repo.Object);
        Sut = Mocker.CreateInstance<ProjectModificationService>();

        // Act
        await Sut.AssignModificationsToReviewer(modificationIds, reviewerId, reviewerEmail, reviewerName);

        // Assert
        repo.Verify(r => r.AssignModificationsToReviewer(modificationIds, reviewerId, reviewerEmail, reviewerName), Times.Once);
    }
}