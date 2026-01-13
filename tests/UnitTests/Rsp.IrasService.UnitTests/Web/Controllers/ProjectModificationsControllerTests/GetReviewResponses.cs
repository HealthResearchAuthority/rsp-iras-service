using Rsp.Service.Application.CQRS.Queries;
using Rsp.Service.Application.DTOS.Responses;
using Rsp.Service.WebApi.Controllers;

namespace Rsp.Service.UnitTests.Web.Controllers.ProjectModificationsControllerTests;

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