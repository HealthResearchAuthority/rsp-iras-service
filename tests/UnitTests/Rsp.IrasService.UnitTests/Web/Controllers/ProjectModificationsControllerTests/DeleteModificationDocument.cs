using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.WebApi.Controllers;

namespace Rsp.Service.UnitTests.Web.Controllers.ProjectModificationsControllerTests;

public class DeleteModificationDocument : TestServiceBase
{
    private readonly ProjectModificationsController _controller;

    public DeleteModificationDocument()
    {
        _controller = Mocker.CreateInstance<ProjectModificationsController>();
    }

    [Theory, AutoData]
    public async Task DeleteModificationDocument_SendsExpectedCommand(
    List<ModificationDocumentDto> modificationChangeRequest)
    {
        // Arrange
        var mediator = Mocker.GetMock<IMediator>();

        mediator
            .Setup(m => m.Send(It.IsAny<DeleteModificationDocumentsCommand>(), It.IsAny<CancellationToken>()));

        // Act
        await _controller.DeleteDocuments(modificationChangeRequest);

        // Assert
        mediator
           .Verify
           (
               m => m.Send(
                   It.Is<DeleteModificationDocumentsCommand>(cmd =>
                       cmd.ModificationDocumentsRequest == modificationChangeRequest
                   ),
                   It.IsAny<CancellationToken>()
               ),
               Times.Once
           );
    }
}