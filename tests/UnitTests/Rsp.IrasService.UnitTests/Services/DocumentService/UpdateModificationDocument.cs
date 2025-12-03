using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Domain.Entities;
using Rsp.IrasService.Infrastructure;
using Rsp.IrasService.Infrastructure.Repositories;

namespace Rsp.IrasService.UnitTests.Services.DocumentService;

public class UpdateModificationDocument : TestServiceBase<IrasService.Services.DocumentService>
{
    private readonly IrasContext _context;
    private readonly DocumentRepository _documentRepository;

    public UpdateModificationDocument()
    {
        var options = new DbContextOptionsBuilder<IrasContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N")).Options;

        _context = new IrasContext(options);
        _documentRepository = new DocumentRepository(_context);
    }

    [Theory]
    [AutoData]
    public async Task Creates_UpdateModificationDocument_404(ModificationDocumentDto modificationDocumentDto)
    {
        // Arrange
        Mocker.Use<IDocumentRepository>(_documentRepository);
        Sut = Mocker.CreateInstance<IrasService.Services.DocumentService>();

        // Act
        var result = await Sut.UpdateModificationDocument(modificationDocumentDto);

        // Assert
        result.ShouldBe(StatusCodes.Status404NotFound);
    }

    [Fact]
    public async Task Creates_UpdateModificationDocument_Correctly()
    {
        // Arrange
        Mocker.Use<IDocumentRepository>(_documentRepository);
        Sut = Mocker.CreateInstance<IrasService.Services.DocumentService>();

        var existingDocument = new ModificationDocument
        {
            Id = Guid.NewGuid(),
            ProjectModificationId = Guid.NewGuid(),
            UserId = "person-123",
            ProjectRecordId = "record-456",
            DocumentStoragePath = "original/path.pdf",
            FileName = "original.pdf",
            FileSize = 1234,
            IsMalwareScanSuccessful = true,
            CreatedDate = DateTime.UtcNow
        };

        _context.ModificationDocuments.Add(existingDocument);
        await _context.SaveChangesAsync();

        var modificationDocumentDto = new ModificationDocumentDto
        {
            Id = existingDocument.Id,
            DocumentStoragePath = "original/path.pdf",
            FileName = existingDocument.FileName,
            UserId = existingDocument.UserId,
            ProjectRecordId = existingDocument.ProjectRecordId,
            ProjectModificationId = existingDocument.ProjectModificationId,
            Status = "Clean",
        };

        // Act
        var result = await Sut.UpdateModificationDocument(modificationDocumentDto);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBe(200);

        var updated = await _context.ModificationDocuments.FindAsync(existingDocument.Id);
        updated.ShouldNotBeNull();
        updated!.DocumentStoragePath.ShouldBe("original/path.pdf");
    }
}