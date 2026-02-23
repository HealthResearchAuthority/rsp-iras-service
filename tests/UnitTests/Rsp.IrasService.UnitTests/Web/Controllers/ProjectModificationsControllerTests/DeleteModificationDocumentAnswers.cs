using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.WebApi.Controllers;

namespace Rsp.Service.UnitTests.Web.Controllers.ProjectModificationsControllerTests;

public class DeleteModificationDocumentAnswers : TestServiceBase
{
    private readonly ProjectModificationsController _controller;

    public DeleteModificationDocumentAnswers()
    {
        _controller = Mocker.CreateInstance<ProjectModificationsController>();
    }

    [Theory, AutoData]
    public async Task DeleteModificationDocumentAnswers_SendsExpectedCommand(
    List<ModificationDocumentDto> modificationChangeRequest)
    {
        // Arrange
        var mediator = Mocker.GetMock<IMediator>();

        mediator
            .Setup(m => m.Send(It.IsAny<DeleteModificationDocumentAnswersCommand>(), It.IsAny<CancellationToken>()));

        // Act
        await _controller.DeleteDocumentAnswers(modificationChangeRequest);

        // Assert
        mediator
           .Verify
           (
               m => m.Send(
                   It.Is<DeleteModificationDocumentAnswersCommand>(cmd =>
                       cmd.ModificationDocumentsRequest == modificationChangeRequest
                   ),
                   It.IsAny<CancellationToken>()
               ),
               Times.Once
           );
    }
}