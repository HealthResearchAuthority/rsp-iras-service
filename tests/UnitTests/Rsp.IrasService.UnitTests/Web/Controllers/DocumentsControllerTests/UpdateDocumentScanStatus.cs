using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.IrasService.WebApi.Controllers;

namespace Rsp.IrasService.UnitTests.Web.Controllers.DocumentsControllerTests;

public class UpdateDocumentScanStatusTests : TestServiceBase<DocumentsController>
{
    [Theory]
    [AutoData]
    public async Task UpdateDocumentScanStatus_ReturnsOk_OnMediator200(ModificationDocumentDto dto)
    {
        // Arrange
        dto.Id = Guid.NewGuid();
        dto.DocumentStoragePath = "folder/file.pdf";

        var mockMediator = Mocker.GetMock<IMediator>();
        mockMediator
            .Setup(m => m.Send(
                It.Is<UpdateModificationDocumentCommand>(c => c.ModificationDocumentsRequest == dto),
                default))
            .ReturnsAsync(StatusCodes.Status200OK);

        // Act
        var result = await Sut.UpdateDocumentScanStatus(dto);

        // Assert
        result.ShouldBeOfType<OkObjectResult>();
        var ok = (OkObjectResult)result;
        ok.Value.ShouldBeOfType<UpdateDocumentScanStatusResponse>();

        var payload = (UpdateDocumentScanStatusResponse)ok.Value!;
        payload.Id.ShouldBe(dto.Id);
        payload.Status.ShouldBe("success");

        mockMediator.Verify(m => m.Send(
            It.Is<UpdateModificationDocumentCommand>(c => c.ModificationDocumentsRequest == dto),
            CancellationToken.None), Times.Once);
    }

    [Theory]
    [AutoData]
    public async Task UpdateDocumentScanStatus_ReturnsNotFound_OnMediator404(ModificationDocumentDto dto)
    {
        // Arrange
        dto.Id = Guid.NewGuid();
        dto.DocumentStoragePath = "folder/file.pdf";

        var mockMediator = Mocker.GetMock<IMediator>();
        mockMediator
            .Setup(m => m.Send(
                It.Is<UpdateModificationDocumentCommand>(c => c.ModificationDocumentsRequest == dto),
                CancellationToken.None))
            .ReturnsAsync(StatusCodes.Status404NotFound);

        // Act
        var result = await Sut.UpdateDocumentScanStatus(dto);

        // Assert
        result.ShouldBeOfType<NotFoundObjectResult>();
        var nf = (NotFoundObjectResult)result;
        nf.Value.ShouldBeOfType<UpdateDocumentScanStatusResponse>();

        var payload = (UpdateDocumentScanStatusResponse)nf.Value!;
        payload.Id.ShouldBe(dto.Id);
        payload.Status.ShouldBe("failure");
        payload.ErrorResponse.ShouldNotBeNull();
        payload.ErrorResponse!.Code.ShouldBe("NOT_FOUND");
        payload.ErrorResponse!.Message.ShouldBe("Document could not be found using document storage path.");

        mockMediator.Verify(m => m.Send(
            It.Is<UpdateModificationDocumentCommand>(c => c.ModificationDocumentsRequest == dto),
            CancellationToken.None), Times.Once);
    }

    [Theory]
    [AutoData]
    public async Task UpdateDocumentScanStatus_ReturnsBadRequest_OnValidationError(ModificationDocumentDto dto)
    {
        // Arrange (invalid)
        dto.Id = Guid.Empty;
        dto.DocumentStoragePath = "";

        // Act
        var result = await Sut.UpdateDocumentScanStatus(dto);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBeOfType<BadRequestObjectResult>();
        var bad = (BadRequestObjectResult)result;
        bad.Value.ShouldBeOfType<UpdateDocumentScanStatusResponse>();

        var payload = (UpdateDocumentScanStatusResponse)bad.Value!;
        payload.Id.ShouldBe(dto.Id);
        payload.Status.ShouldBe("failure");
        payload.ErrorResponse.ShouldNotBeNull();
        payload.ErrorResponse!.Code.ShouldBe("VALIDATION_ERROR");
        payload.ErrorResponse!.Message.ShouldBe("DocumentStoragePath must be provided.");

        // Mediator must NOT be called
        Mocker.GetMock<IMediator>().Verify(
            m => m.Send(It.IsAny<UpdateModificationDocumentCommand>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Theory]
    [AutoData]
    public async Task UpdateDocumentScanStatus_ReturnsInternalServerError_OnMediatorThrows(ModificationDocumentDto dto)
    {
        // Arrange
        dto.Id = Guid.NewGuid();
        dto.DocumentStoragePath = "folder/file.pdf";

        var ex = new InvalidOperationException("Boom!");
        var mockMediator = Mocker.GetMock<IMediator>();
        mockMediator
            .Setup(m => m.Send(
                It.Is<UpdateModificationDocumentCommand>(c => c.ModificationDocumentsRequest == dto),
                CancellationToken.None))
            .ThrowsAsync(ex);

        // Act
        var result = await Sut.UpdateDocumentScanStatus(dto);

        // Assert
        result.ShouldBeOfType<ObjectResult>();
        var obj = (ObjectResult)result;
        obj.StatusCode.ShouldBe(StatusCodes.Status500InternalServerError);

        obj.Value.ShouldBeOfType<UpdateDocumentScanStatusResponse>();
        var payload = (UpdateDocumentScanStatusResponse)obj.Value!;
        payload.Id.ShouldBe(dto.Id);
        payload.Status.ShouldBe("failure");
        payload.ErrorResponse.ShouldNotBeNull();
        payload.ErrorResponse!.Code.ShouldBe("INTERNAL_SERVER_ERROR");
        payload.ErrorResponse!.Message.ShouldBe("Boom!");
        payload.ErrorResponse!.Details.ShouldContain("InvalidOperationException");

        mockMediator.Verify(m => m.Send(
            It.Is<UpdateModificationDocumentCommand>(c => c.ModificationDocumentsRequest == dto),
            CancellationToken.None), Times.Once);
    }

    [Theory]
    [AutoData]
    public async Task UpdateDocumentScanStatus_SendsCommand_WithExactDto(ModificationDocumentDto dto)
    {
        // Arrange (interaction test)
        dto.Id = Guid.NewGuid();
        dto.DocumentStoragePath = "folder/file.pdf";

        var mockMediator = Mocker.GetMock<IMediator>();
        mockMediator
            .Setup(m => m.Send(It.IsAny<UpdateModificationDocumentCommand>(), CancellationToken.None))
            .ReturnsAsync(StatusCodes.Status200OK);

        // Act
        await Sut.UpdateDocumentScanStatus(dto);

        // Assert
        mockMediator.Verify(m => m.Send(
            It.Is<UpdateModificationDocumentCommand>(c => c.ModificationDocumentsRequest == dto),
            CancellationToken.None), Times.Once);
    }
}