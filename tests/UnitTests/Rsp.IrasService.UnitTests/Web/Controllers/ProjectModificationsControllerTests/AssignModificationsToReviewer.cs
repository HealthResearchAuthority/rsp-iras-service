using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.WebApi.Controllers;

namespace Rsp.IrasService.UnitTests.Web.Controllers.ProjectModificationsControllerTests;

public class AssignModificationsToReviewer : TestServiceBase<ProjectModificationsController>
{
    [Theory, AutoData]
    public async Task AssignModificationsToReviewer_ShouldReturnOk_WhenAssignmentsAreSuccessful
    (
        List<string> modificationIds,
        string reviewerId
    )
    {
        // Arrange
        var mockMediator = Mocker.GetMock<IMediator>();
        mockMediator.Setup(m => m.Send(It.IsAny<AssignModificationsToReviewerCommand>(), default));

        // Act
        await Sut.AssignModificationsToReviewer(modificationIds, reviewerId);
        // Assert

        mockMediator
            .Verify
            (
                m => m.Send
                (
                    It.Is<AssignModificationsToReviewerCommand>
                    (
                        cmd => cmd.ModificationIds == modificationIds &&
                        cmd.ReviewerId == reviewerId
                    ),
                    default
                ),
                Times.Once
            );
    }
}