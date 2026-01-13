using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Application.DTOS.Responses;
using Rsp.Service.WebApi.Controllers;

namespace Rsp.Service.UnitTests.Web.Controllers.ProjectModificationsControllerTests;

public class CreateModification : TestServiceBase<ProjectModificationsController>
{
    [Theory, AutoData]
    public async Task CreateModification_ReturnsExpectedResponse
    (
        ModificationRequest modificationRequest,
        ModificationResponse modificationResponse
    )
    {
        // Arrange
        var mediator = Mocker.GetMock<IMediator>();

        mediator
            .Setup(m => m.Send(It.IsAny<CreateModificationCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(modificationResponse);

        // Act
        var result = await Sut.CreateModification(modificationRequest);

        // Assert
        result.ShouldBeEquivalentTo(modificationResponse);

        mediator
            .Verify
            (m => m
                .Send
                (
                    It.Is<CreateModificationCommand>(cmd => cmd.ModificationRequest == modificationRequest),
                    It.IsAny<CancellationToken>()
                ),
                Times.Once
            );
    }
}