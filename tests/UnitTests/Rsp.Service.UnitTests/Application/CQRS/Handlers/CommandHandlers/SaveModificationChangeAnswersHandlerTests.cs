using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.Application.CQRS.Handlers.CommandHandlers;
using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.Service.UnitTests.Application.CQRS.Handlers.CommandHandlers;

public class SaveModificationChangeAnswersHandlerTests
{
    [Theory, AutoData]
    public async Task Handle_Delegates_To_RespondentService(ModificationChangeAnswersRequest request)
    {
        // Arrange
        var respondentService = new Mock<IRespondentService>();
        var handler = new SaveModificationChangeAnswersHandler(respondentService.Object);
        var cmd = new SaveModificationChangeAnswersCommand(request);

        // Act
        await handler.Handle(cmd, CancellationToken.None);

        // Assert
        respondentService.Verify(s => s.SaveModificationChangeAnswers(request), Times.Once);
    }
}
