using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Services;

namespace Rsp.IrasService.UnitTests.Services.ProjectModificationServiceTests;

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