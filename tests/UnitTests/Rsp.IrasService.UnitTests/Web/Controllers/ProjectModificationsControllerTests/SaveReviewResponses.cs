using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.WebApi.Controllers;

namespace Rsp.Service.UnitTests.Web.Controllers.ProjectModificationsControllerTests;

public class SaveReviewResponses : TestServiceBase<ProjectModificationsController>
{
    [Theory, AutoData]
    public async Task SaveReviewResponses_ShouldReturnOk_WhenValidRequest
    (
        ModificationReviewRequest request
    )
    {
        // Arrange
        var mockMediator = Mocker.GetMock<IMediator>();
        mockMediator.Setup(m => m.Send(It.IsAny<SaveModificationReviewResponsesCommand>(), default));

        // Act
        await Sut.SaveReviewResponses(request);

        // Assert
        mockMediator
            .Verify
            (
                m => m.Send
                (
                    It.Is<SaveModificationReviewResponsesCommand>
                    (
                        cmd => cmd.ModificationReviewRequest == request
                    ),
                    default
                ),
                Times.Once
            );
    }
}