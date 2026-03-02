using Microsoft.EntityFrameworkCore;
using Rsp.Service.Application.Constants;
using Rsp.Service.Application.Contracts.Repositories;
using Rsp.Service.Application.DTOS.Requests;
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

    public async Task<IEnumerable<ModificationDocumentsAuditTrailDto>> GetProjectDocumentsAuditTrail
    (
        int pageNumber,
        int pageSize,
        string sortField,
        string sortDirection,
        string projectRecordId,
        bool includeTotalCount = false
    )
    {
        var modifications = await (from audit in irasContext.ModificationDocumentsAuditTrail
                                   join mod in irasContext.ProjectModifications
                                   on audit.ProjectModificationId equals mod.Id
                                   where mod.ProjectRecordId == projectRecordId
                                   orderby audit.DateTimeStamp descending
                                   select new ModificationDocumentsAuditTrailDto
                                   {
                                       Id = audit.Id,
                                       ProjectModificationId = audit.ProjectModificationId,
                                       DateTimeStamp = audit.DateTimeStamp,
                                       Description = audit.Description,
                                       FileName = audit.FileName,
                                       User = audit.User,
                                       ModificationIdentifier = mod.ModificationIdentifier,
                                       ModificationNumber = mod.ModificationNumber
                                   }).ToListAsync();

        if (includeTotalCount)
        {
            return modifications;
        }

        Func<ModificationDocumentsAuditTrailDto, object>? keySelector = sortField switch
        {
            nameof(ModificationDocumentsAuditTrailDto.DateTimeStamp) => x => x.DateTimeStamp,
            nameof(ModificationDocumentsAuditTrailDto.FileName) => x => x.FileName.ToLowerInvariant(),
            nameof(ModificationDocumentsAuditTrailDto.ModificationIdentifier) => x => x.ModificationNumber,
            nameof(ModificationDocumentsAuditTrailDto.Description) => x => x.Description.ToLowerInvariant(),
            nameof(ModificationDocumentsAuditTrailDto.User) => x => x.User,
            _ => null
        };

        if (keySelector == null)
        {
            modifications.OrderByDescending(x => x.DateTimeStamp);
        }

        modifications = [.. string.Equals(sortDirection, "desc", StringComparison.OrdinalIgnoreCase)
            ? modifications.OrderByDescending(keySelector!)
            : modifications.OrderBy(keySelector!)];

        return modifications
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);
    }
}