using Rsp.Service.Application.Contracts.Repositories;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Services;

namespace Rsp.Service.UnitTests.Services.ProjectModificationServiceTests;

public class SaveReviewResponsesTests : TestServiceBase<ProjectModificationService>
{
    [Theory, AutoData]
    public async Task SaveReviewResponses_Calls_Repository
    (
        ModificationReviewRequest modificationReviewRequest
    )
    {
        var repo = new Mock<IProjectModificationRepository>();
        repo.Setup(r => r.SaveModificationReviewResponses(It.IsAny<ModificationReviewRequest>()))
            .Returns(Task.CompletedTask)
            .Verifiable();

        Mocker.Use(repo.Object);
        Sut = Mocker.CreateInstance<ProjectModificationService>();

        await Sut.SaveModificationReviewResponses(modificationReviewRequest);

        repo.Verify(r => r.SaveModificationReviewResponses(modificationReviewRequest), Times.Once());
    }
}