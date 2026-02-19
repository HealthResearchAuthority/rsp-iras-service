using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Rsp.Service.Application.Contracts.Repositories;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Services;

namespace Rsp.Service.UnitTests.Services.ProjectModificationServiceTests;

public class SaveReviewResponsesTests : TestServiceBase<ProjectModificationService>
{
    [Theory, AutoData]
    public async Task SaveReviewResponses_Calls_Repository
    (
        ModificationReviewRequest modificationReviewRequest,
        ClaimsPrincipal user,
        Guid userId
    )
    {
        var claimsIdentity = new ClaimsIdentity();
        claimsIdentity.AddClaim(new Claim("userId", userId.ToString()));
        user.AddIdentity(claimsIdentity);
        var httpContextAccessor = Mocker.GetMock<IHttpContextAccessor>();
        httpContextAccessor.Setup(h => h.HttpContext)
            .Returns(new DefaultHttpContext()
            {
                User = user
            });

        var repo = new Mock<IProjectModificationRepository>();
        repo.Setup(r => r.SaveModificationReviewResponses(It.IsAny<ModificationReviewRequest>(), It.IsAny<Guid>()))
            .Returns(Task.CompletedTask)
            .Verifiable();

        Mocker.Use(repo.Object);
        Sut = Mocker.CreateInstance<ProjectModificationService>();

        await Sut.SaveModificationReviewResponses(modificationReviewRequest);

        repo.Verify(r => r.SaveModificationReviewResponses(modificationReviewRequest, It.IsAny<Guid>()), Times.Once());
    }
}