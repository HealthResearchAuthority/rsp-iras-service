using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.WebApi.Controllers;

namespace Rsp.Service.UnitTests.Web.Controllers.ProjectModificationsControllerTests;

public class AssignModificationsToReviewer : TestServiceBase<ProjectModificationsController>
{
    [Theory, AutoData]
    public async Task AssignModificationsToReviewer_ShouldReturnOk_WhenAssignmentsAreSuccessful
    (
        List<string> modificationIds,
        string reviewerId,
        string reviewerEmail,
        string reviewerName
    )
    {
        // Arrange
        var mockMediator = Mocker.GetMock<IMediator>();
        mockMediator.Setup(m => m.Send(It.IsAny<AssignModificationsToReviewerCommand>(), default));

        // Act
        await Sut.AssignModificationsToReviewer(modificationIds, reviewerId, reviewerEmail, reviewerName);
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