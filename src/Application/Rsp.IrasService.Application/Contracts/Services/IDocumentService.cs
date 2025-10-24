using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.Logging.Interceptors;

namespace Rsp.IrasService.Application.Contracts.Services;

/// <summary>
///     Interface to create, read, and update document records in the database.
///     Marked as IInterceptable to enable start/end logging for all methods.
/// </summary>
public interface IDocumentService : IInterceptable
{
    Task<int?> UpdateModificationDocument(ModificationDocumentDto modificationDocumentDto);
}