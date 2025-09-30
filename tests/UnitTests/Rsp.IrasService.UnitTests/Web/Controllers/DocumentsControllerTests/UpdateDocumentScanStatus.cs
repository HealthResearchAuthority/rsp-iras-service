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
    public async Task UpdateDocumentScanStatus_ReturnsOk_OnSuccess(ModificationDocumentDto dto)
    {
        // Arrange
        dto.Id = Guid.NewGuid();
        dto.DocumentStoragePath = "folder/file.pdf";
        dto.CorellationId = Guid.NewGuid().ToString();

        var mockMediator = Mocker.GetMock<IMediator>();
        mockMediator
            .Setup(m => m.Send(
                It.Is<UpdateModificationDocumentCommand>(c => c.ModificationDocumentsRequest == dto),
                CancellationToken.None))
            .ReturnsAsync(1); // non-null → success

        // Act
        var result = await Sut.UpdateDocumentScanStatus(dto);

        // Assert
        result.ShouldBeOfType<OkObjectResult>();
        var ok = (OkObjectResult)result;
        ok.Value.ShouldBeOfType<UpdateDocumentScanStatusResponse>();

        var payload = (UpdateDocumentScanStatusResponse)ok.Value!;
        payload.Id.ShouldBe(dto.Id);
        payload.CorellationId.ShouldBe(dto.CorellationId);
        payload.Status.ShouldBe("success");

        // Verify mediator call
        mockMediator.Verify(m => m.Send(
                It.Is<UpdateModificationDocumentCommand>(c => c.ModificationDocumentsRequest == dto),
                CancellationToken.None),
            Times.Once);
    }

    [Theory]
    [AutoData]
    public async Task UpdateDocumentScanStatus_ReturnsBadRequest_OnValidationError(ModificationDocumentDto dto)
    {
        // Arrange (invalid)
        dto.Id = Guid.Empty;
        dto.DocumentStoragePath = "";
        dto.CorellationId = Guid.NewGuid().ToString();

        // Act
        var result = await Sut.UpdateDocumentScanStatus(dto);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBeOfType<BadRequestObjectResult>();
        var bad = (BadRequestObjectResult)result;
        bad.Value.ShouldBeOfType<UpdateDocumentScanStatusResponse>();

        var payload = (UpdateDocumentScanStatusResponse)bad.Value!;
        payload.Id.ShouldBe(dto.Id);
        payload.CorellationId.ShouldBe(dto.CorellationId);
        payload.Status.ShouldBe("failure");
        payload.ErrorResponse.ShouldNotBeNull();
        payload.ErrorResponse!.Code.ShouldBe("VALIDATION_ERROR");
        payload.ErrorResponse!.Message.ShouldBe("Either Id or DocumentStoragePath must be provided.");

        // Mediator must NOT be called
        Mocker.GetMock<IMediator>().Verify(
            m => m.Send(It.IsAny<UpdateModificationDocumentCommand>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Theory]
    [AutoData]
    public async Task UpdateDocumentScanStatus_ReturnsBadRequest_WhenMediatorReturnsNull(ModificationDocumentDto dto)
    {
        // Arrange (valid dto but mediator returns null → server error path)
        dto.Id = Guid.NewGuid();
        dto.DocumentStoragePath = "folder/file.pdf";
        dto.CorellationId = Guid.NewGuid().ToString();

        var mockMediator = Mocker.GetMock<IMediator>();
        mockMediator
            .Setup(m => m.Send(
                It.Is<UpdateModificationDocumentCommand>(c => c.ModificationDocumentsRequest == dto),
                CancellationToken.None));

        // Act
        var result = await Sut.UpdateDocumentScanStatus(dto);

        // Assert
        result.ShouldBeOfType<BadRequestObjectResult>();
        var bad = (BadRequestObjectResult)result;
        bad.Value.ShouldBeOfType<UpdateDocumentScanStatusResponse>();

        var payload = (UpdateDocumentScanStatusResponse)bad.Value!;
        payload.Id.ShouldBe(dto.Id);
        payload.CorellationId.ShouldBe(dto.CorellationId);
        payload.Status.ShouldBe("failure");
        payload.ErrorResponse.ShouldNotBeNull();
        payload.ErrorResponse!.Code.ShouldBe("SERVER_ERROR");
        payload.ErrorResponse!.Message.ShouldBe("Unexpected server error.");

        // Verify mediator call happened once
        mockMediator.Verify(m => m.Send(
                It.Is<UpdateModificationDocumentCommand>(c => c.ModificationDocumentsRequest == dto),
                CancellationToken.None),
            Times.Once);
    }

    [Theory]
    [AutoData]
    public async Task UpdateDocumentScanStatus_SendsCommand_WithExactDto(ModificationDocumentDto dto)
    {
        // Arrange (pure interaction test)
        dto.Id = Guid.NewGuid();
        dto.DocumentStoragePath = "folder/file.pdf";
        dto.CorellationId = Guid.NewGuid().ToString();

        var mockMediator = Mocker.GetMock<IMediator>();
        mockMediator
            .Setup(m => m.Send(
                It.IsAny<UpdateModificationDocumentCommand>(),
                CancellationToken.None))
            .ReturnsAsync(1);

        // Act
        await Sut.UpdateDocumentScanStatus(dto);

        // Assert
        mockMediator.Verify(m => m.Send(
                It.Is<UpdateModificationDocumentCommand>(c => c.ModificationDocumentsRequest == dto),
                CancellationToken.None),
            Times.Once);
    }
}