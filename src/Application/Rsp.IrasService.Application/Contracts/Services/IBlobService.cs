using Azure.Storage.Blobs;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Logging.Interceptors;

namespace Rsp.Service.Application.Contracts.Services;

/// <summary>
///     Interface to create, read, and update document records in the database.
///     Marked as IInterceptable to enable start/end logging for all methods.
/// </summary>
public interface IBlobService
{
    Task CopyBlobWithinContainerAsync(
        string sourceBlobName, // e.g. "323477/2611817b-.../TEST.txt"
        string destBlobName, // e.g. "323477/<newGuid>/TEST.txt"
        CancellationToken ct = default);
}