using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Domain.Entities;
using Rsp.IrasService.Services;

namespace Rsp.IrasService.UnitTests.Services.ProjectModificationServiceTests;

public class GetReviewResponsesTests : TestServiceBase<ProjectModificationService>
{
    [Theory, AutoData]
    public async Task Returns_ReviewResponses_For_Given_ModificationId
    (
        Guid projectModificationId,
        ProjectModification repoResponse
    )
    {
        // Arrange
        var repo = Mocker.GetMock<IProjectModificationRepository>();
        repo.Setup(r => r.GetModificationById(It.IsAny<Guid>())).ReturnsAsync(repoResponse);

        // Act
        Sut = Mocker.CreateInstance<ProjectModificationService>();
        var result = await Sut.GetModificationReviewResponses(projectModificationId);

        // Assert
        result.ShouldNotBeNull();
        result.ModificationId.ShouldBe(repoResponse.Id);
        result.Comment.ShouldBe(repoResponse.ReviewerComments);
        result.ReasonNotApproved.ShouldBe(repoResponse.ReasonNotApproved);
        result.ReviewOutcome.ShouldBe(repoResponse.ProvisionalReviewOutcome);
    }
}