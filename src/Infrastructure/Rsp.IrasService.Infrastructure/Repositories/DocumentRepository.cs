using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Infrastructure.Repositories;

/// <summary>
///     Repository for managing project personnel responses and modification responses.
/// </summary>
public class DocumentRepository(IrasContext irasContext) : IDocumentRepository
{
    public async Task<int?> UpdateModificationDocument(ModificationDocument modificationDocument)
    {
        ModificationDocument? existing;

        if (modificationDocument.Id != Guid.Empty)
        {
            existing = await irasContext.ModificationDocuments
                .FirstOrDefaultAsync(d => d.Id == modificationDocument.Id);
        }
        else
        {
            existing = await irasContext.ModificationDocuments
                .FirstOrDefaultAsync(d => d.DocumentStoragePath == modificationDocument.DocumentStoragePath);
        }

        if (existing is null)
        {
            return null;
        }

        if (!string.IsNullOrWhiteSpace(modificationDocument.DocumentStatus))
        {
            existing.DocumentStatus = modificationDocument.DocumentStatus;
        }

        if (!string.IsNullOrWhiteSpace(modificationDocument.DocumentStoragePath))
        {
            existing.DocumentStoragePath = modificationDocument.DocumentStoragePath;
        }

        return await irasContext.SaveChangesAsync();
    }
}