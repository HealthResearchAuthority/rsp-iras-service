using Rsp.Service.Application.Contracts.Repositories;
using Rsp.Service.Application.Specifications;
using Rsp.Service.Domain.Entities;
using Rsp.Service.Services;

namespace Rsp.Service.UnitTests.Services.ProjectModificationServiceTests;

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

        repo
            .Setup(r => r.GetModification(It.IsAny<GetModificationSpecification>()))
            .ReturnsAsync(repoResponse);

        // Act
        Sut = Mocker.CreateInstance<ProjectModificationService>();
        var result = await Sut.GetModificationReviewResponses("PR1", projectModificationId);

        // Assert
        result.ShouldNotBeNull();
        result.ModificationId.ShouldBe(repoResponse.Id);
        result.Comment.ShouldBe(repoResponse.ReviewerComments);
        result.ReasonNotApproved.ShouldBe(repoResponse.ReasonNotApproved);
        result.ReviewOutcome.ShouldBe(repoResponse.ProvisionalReviewOutcome);
        result.RevisionDescription.ShouldBe(repoResponse.RevisionDescription);
    }
}