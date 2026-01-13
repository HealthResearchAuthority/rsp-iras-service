using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Constants;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Infrastructure.Repositories;

/// <summary>
///     Repository for managing project personnel responses and modification responses.
/// </summary>
public class DocumentRepository(IrasContext irasContext) : IDocumentRepository
{
    public async Task<int?> UpdateModificationDocumentStatus(ModificationDocument modificationDocument)
    {
        // change to status code
        var existing = await irasContext.ModificationDocuments
            .FirstOrDefaultAsync(d => d.DocumentStoragePath == modificationDocument.DocumentStoragePath);

        if (existing is null)
        {
            return 404;
        }

        existing.IsMalwareScanSuccessful = modificationDocument.IsMalwareScanSuccessful;
        if (modificationDocument.IsMalwareScanSuccessful == false)
        {
            existing.Status = "Failed";
        }

        // Create audit trail entry
        var auditDescription = modificationDocument.IsMalwareScanSuccessful == true
            ? DocumentAuditEvents.MalwareScanSuccessful
            : DocumentAuditEvents.MalwareScanUnsuccessful;

        var auditEntry = new ModificationDocumentsAuditTrail
        {
            Id = Guid.NewGuid(),
            ProjectModificationId = existing.ProjectModificationId,
            DateTimeStamp = DateTime.UtcNow,
            Description = auditDescription,
            FileName = existing.FileName,
            User = existing.UserId
        };

        irasContext.ModificationDocumentsAuditTrail.Add(auditEntry);

        await irasContext.SaveChangesAsync();
        return 200;
    }
}