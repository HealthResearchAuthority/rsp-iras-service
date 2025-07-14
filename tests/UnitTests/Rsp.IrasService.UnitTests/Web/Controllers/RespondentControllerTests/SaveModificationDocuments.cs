using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.WebApi.Controllers;

namespace Rsp.IrasService.UnitTests.Web.Controllers.RespondentControllerTests;

public class SaveModificationDocuments : TestServiceBase<RespondentController>
{
    [Theory, AutoData]
    public async Task SaveModificationDocuments_SendsCommand(ModificationDocumentDto request)
    {
        // Arrange
        var mediator = Mocker.GetMock<IMediator>();

        mediator
            .Setup(m => m.Send(It.IsAny<SaveModificationDocumentsCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        await Sut.SaveModificationDocuments(request);

        // Assert
        mediator
            .Verify
            (m => m
                .Send
                (
                    It.Is<SaveModificationDocumentsCommand>(cmd => cmd.ModificationDocumentRequest == request),
                    It.IsAny<CancellationToken>()
                ),
                Times.Once
            );
    }
}