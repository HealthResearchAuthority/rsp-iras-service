using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.WebApi.Controllers;

namespace Rsp.IrasService.UnitTests.Web.Controllers.ProjectModificationsControllerTests;

public class CreateModificationDocument : TestServiceBase<ProjectModificationsController>
{
    [Theory, AutoData]
    public async Task CreateModificationDocument_ReturnsExpectedResponse
    (
        List<ModificationDocumentDto> modificationChangeRequest
    )
    {
        // Arrange
        var mediator = Mocker.GetMock<IMediator>();

        mediator
            .Setup(m => m.Send(It.IsAny<SaveModificationDocumentsCommand>(), It.IsAny<CancellationToken>()));

        // Act
        await Sut.CreateModificationDocument(modificationChangeRequest);

        // Assert
        mediator
           .Verify
           (m => m
               .Send
           (
                   It.Is<SaveModificationDocumentsCommand>(cmd => cmd.ModificationDocumentsRequest == modificationChangeRequest),
                   It.IsAny<CancellationToken>()
               ),
               Times.Once
           );
    }
}