using Microsoft.AspNetCore.Mvc;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.WebApi.Controllers;

namespace Rsp.IrasService.UnitTests.Web.Controllers.DocumentsControllerTests;

public class UpdateDocumentScanStatusTests : TestServiceBase<DocumentsController>
{
    [Theory]
    [AutoData]
    public async Task UpdateDocumentScanStatus_ShouldSendCommand_AndReturnOk(ModificationDocumentDto dto)
    {
        // Arrange
        dto.Id = Guid.NewGuid();
        dto.DocumentStoragePath = "valid/path/to/document.pdf";

        var mockMediator = Mocker.GetMock<IMediator>();
        mockMediator
            .Setup(m => m.Send(It.Is<UpdateModificationDocumentCommand>(c => c.ModificationDocumentsRequest == dto),
                CancellationToken.None));

        // Act
        var result = await Sut.UpdateDocumentScanStatus(dto);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBeOfType<OkResult>();

        // Verify
        mockMediator.Verify
        (
            m => m.Send(
                It.Is<UpdateModificationDocumentCommand>(c => c.ModificationDocumentsRequest == dto),
                CancellationToken.None
            ),
            Times.Once
        );
    }

    [Theory]
    [AutoData]
    public async Task UpdateDocumentScanStatus_ShouldReturnBadRequest_WhenInvalidDto(ModificationDocumentDto dto)
    {
        // Arrange
        dto.Id = Guid.Empty;
        dto.DocumentStoragePath = string.Empty;

        // Act
        var result = await Sut.UpdateDocumentScanStatus(dto);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBeOfType<BadRequestObjectResult>();
        (result as BadRequestObjectResult)!.Value.ShouldBe("Either Id or DocumentStoragePath must be provided.");

        // Verify mediator is never called
        Mocker.GetMock<IMediator>().Verify(
            m => m.Send(It.IsAny<UpdateModificationDocumentCommand>(), default),
            Times.Never
        );
    }
}