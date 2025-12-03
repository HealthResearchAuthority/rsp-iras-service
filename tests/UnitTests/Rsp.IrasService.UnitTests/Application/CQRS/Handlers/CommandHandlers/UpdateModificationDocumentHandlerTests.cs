using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.CQRS.Handlers.CommandHandlers;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.UnitTests.Application.CQRS.Handlers.CommandHandlers;

public class UpdateModificationDocumentHandlerTests
{
    private readonly UpdateModificationDocumentHandler _handler;
    private readonly Mock<IDocumentService> _documentServiceMock;

    public UpdateModificationDocumentHandlerTests()
    {
        _documentServiceMock = new Mock<IDocumentService>();
        _handler = new UpdateModificationDocumentHandler(_documentServiceMock.Object);
    }

    [Fact]
    public async Task Handle_UpdateModificationDocumentCommand_ShouldReturnAffectedRows()
    {
        // Arrange
        var dto = new ModificationDocumentDto
        {
            Id = Guid.NewGuid(),
            ProjectModificationId = Guid.NewGuid(),
            UserId = "person-123",
            ProjectRecordId = "record-456",
            DocumentStoragePath = "updated/path.pdf",
            FileName = "doc.pdf"
        };

        int? expected = 200;
        var command = new UpdateModificationDocumentCommand(dto);

        _documentServiceMock
            .Setup(s => s.UpdateModificationDocument(dto))
            .ReturnsAsync(expected);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBe(expected);

        _documentServiceMock.Verify(
            s => s.UpdateModificationDocument(It.IsAny<ModificationDocumentDto>()),
            Times.Once);
    }
}