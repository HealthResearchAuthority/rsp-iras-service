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
        // change to status code
        var existing = await irasContext.ModificationDocuments
           .FirstOrDefaultAsync(d => d.DocumentStoragePath == modificationDocument.DocumentStoragePath);

        if (existing is null)
        {
            return 404;
        }

        if (!string.IsNullOrWhiteSpace(modificationDocument.DocumentStoragePath))
        {
            existing.DocumentStoragePath = modificationDocument.DocumentStoragePath;
        }

        await irasContext.SaveChangesAsync();
        return 200;
    }
}