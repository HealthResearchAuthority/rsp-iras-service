using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Azure;
using Rsp.Service.Application.Contracts.Services;

namespace Rsp.Service.Services;

/// <summary>
///     Service for managing modification documents for project records.
/// </summary>
public class BlobService(IAzureClientFactory<BlobServiceClient> clientFactory) : IBlobService
{
    private const string ClientName = "Clean";     // DI client registration name (your choice)
    private const string ContainerName = "clean";  // Azure container name (MUST be lowercase)

    public async Task CopyBlobWithinContainerAsync(
        string sourceBlobName,   // e.g. "323477/2611817b-.../TEST.txt"
        string destBlobName,     // e.g. "323477/<newGuid>/TEST.txt"
        bool overwrite = false,
        bool deleteSourceAfterCopy = false,
        CancellationToken ct = default)
    {
        // If callers accidentally pass URL-encoded names (e.g. %20), normalize them.
        sourceBlobName = Uri.UnescapeDataString(sourceBlobName);
        destBlobName = Uri.UnescapeDataString(destBlobName);

        var blobServiceClient = clientFactory.CreateClient(ClientName);
        var container = blobServiceClient.GetBlobContainerClient(ContainerName);

        var source = container.GetBlobClient(sourceBlobName);
        var dest = container.GetBlobClient(destBlobName);

        // Optional: quick container existence check to fail with a clearer error
        if (!await container.ExistsAsync(ct))
            throw new InvalidOperationException($"Container not found or not accessible: {ContainerName}");

        if (!await source.ExistsAsync(ct))
            throw new InvalidOperationException($"Source blob not found: {sourceBlobName}");

        if (!overwrite && await dest.ExistsAsync(ct))
            throw new InvalidOperationException($"Destination blob already exists: {destBlobName}");

        await dest.StartCopyFromUriAsync(source.Uri, cancellationToken: ct);

        while (true)
        {
            var props = await dest.GetPropertiesAsync(cancellationToken: ct);
            var status = props.Value.CopyStatus;

            if (status == CopyStatus.Success) break;

            if (status is CopyStatus.Aborted or CopyStatus.Failed)
                throw new InvalidOperationException(
                    $"Copy failed ({status}). {props.Value.CopyStatusDescription}");

            await Task.Delay(250, ct);
        }

        if (deleteSourceAfterCopy)
            await source.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots, cancellationToken: ct);
    }
}