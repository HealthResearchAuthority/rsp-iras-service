using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.IrasService.WebApi.Controllers;

namespace Rsp.IrasService.UnitTests.Web.Controllers.ProjectModificationsControllerTests;

public class CreateModificationChange : TestServiceBase<ProjectModificationsController>
{
    [Theory, AutoData]
    public async Task CreateModificationChange_ReturnsExpectedResponse(ModificationChangeRequest modificationChangeRequest, ModificationChangeResponse modificationChangeResponse)
    {
        // Arrange
        var mediator = Mocker.GetMock<IMediator>();

        mediator
            .Setup(m => m.Send(It.IsAny<SaveModificationChangeCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(modificationChangeResponse);

        // Act
        var result = await Sut.CreateModificationChange(modificationChangeRequest);

        // Assert

        result.ShouldBeEquivalentTo(modificationChangeResponse);

        mediator
            .Verify
            (m => m
                .Send
                (
                    It.Is<SaveModificationChangeCommand>(cmd => cmd.ModificationChangeRequest == modificationChangeRequest),
                    It.IsAny<CancellationToken>()
                ), Times.Once
            );
    }
}