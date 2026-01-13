using Microsoft.EntityFrameworkCore;
using Rsp.Service.Application.Constants;
using Rsp.Service.Application.Contracts.Repositories;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Infrastructure.Repositories;

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