using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.WebApi.Controllers;

namespace Rsp.Service.UnitTests.Web.Controllers.RespondentControllerTests;

public class SaveModificationDocuments : TestServiceBase<RespondentController>
{
    [Theory, AutoData]
    public async Task SaveModificationDocuments_SendsCommand(List<ModificationDocumentDto> request)
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
                    It.Is<SaveModificationDocumentsCommand>(cmd => cmd.ModificationDocumentsRequest == request),
                    It.IsAny<CancellationToken>()
                ),
                Times.Once
            );
    }
}