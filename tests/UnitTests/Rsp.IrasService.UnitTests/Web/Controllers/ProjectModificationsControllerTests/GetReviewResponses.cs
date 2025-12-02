using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.IrasService.WebApi.Controllers;

namespace Rsp.IrasService.UnitTests.Web.Controllers.ProjectModificationsControllerTests;

public class GetReviewResponses : TestServiceBase<ProjectModificationsController>
{
    [Theory, AutoData]
    public async Task GetReviewResponses_ShouldReturnModificationReviewResponse_WhenValidRequest
    (
        Guid modificationId,
        ModificationReviewResponse expectedResponse
    )
    {
        // Arrange
        var mockMediator = Mocker.GetMock<IMediator>();
        mockMediator.Setup(m => m.Send(It.IsAny<GetModificationReviewResponsesQuery>(), default))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await Sut.GetReviewResponses("PR1", modificationId);

        result.ShouldBeEquivalentTo(expectedResponse);
    }
}