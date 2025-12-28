using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.Application.CQRS.Handlers.CommandHandlers;
using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.Service.UnitTests.Application.CQRS.Handlers.CommandHandlers;

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
            CreatedBy = "person-123",
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