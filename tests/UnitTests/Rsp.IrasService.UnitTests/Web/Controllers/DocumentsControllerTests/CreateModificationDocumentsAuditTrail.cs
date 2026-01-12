using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.WebApi.Controllers;

namespace Rsp.IrasService.UnitTests.Web.Controllers.DocumentsControllerTests;

public class CreateModificationDocumentsAuditTrail : TestServiceBase<DocumentsController>
{
    [Theory]
    [AutoData]
    public async Task CreateModificationDocumentsAuditTrail_SendsCommand_WithExactDtoList(
    List<ModificationDocumentsAuditTrailDto> auditDtos)
    {
        // Arrange
        var mockMediator = Mocker.GetMock<IMediator>();

        mockMediator
            .Setup(m => m.Send(
                It.IsAny<SaveModificationDocumentsAuditTrailCommand>(),
                CancellationToken.None))
            .Returns(Task.CompletedTask);

        // Act
        await Sut.CreateModificationDocumentsAuditTrail(auditDtos);

        // Assert
        mockMediator.Verify(m => m.Send(
            It.Is<SaveModificationDocumentsAuditTrailCommand>(
                c => c.DocumentsAuditTrailRequest == auditDtos),
            CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public async Task CreateModificationDocumentsAuditTrail_WithEmptyList_StillSendsCommand()
    {
        // Arrange
        var auditDtos = new List<ModificationDocumentsAuditTrailDto>();

        var mockMediator = Mocker.GetMock<IMediator>();
        mockMediator
            .Setup(m => m.Send(It.IsAny<SaveModificationDocumentsAuditTrailCommand>(), CancellationToken.None))
            .Returns(Task.CompletedTask);

        // Act
        await Sut.CreateModificationDocumentsAuditTrail(auditDtos);

        // Assert
        mockMediator.Verify(m => m.Send(
            It.Is<SaveModificationDocumentsAuditTrailCommand>(
                c => c.DocumentsAuditTrailRequest.Count == 0),
            CancellationToken.None),
            Times.Once);
    }
}