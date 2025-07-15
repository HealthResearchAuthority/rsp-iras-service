using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.IrasService.WebApi.Controllers;

namespace Rsp.IrasService.UnitTests.Web.Controllers.ProjectModificationsControllerTests;

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