using MediatR;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Queries;

/// <summary>
/// Query to retrieve a modification change by its unique identifier.
/// </summary>
/// <param name="modificationChangeId">The unique identifier of the modification change to retrieve.</param>
public class GetModificationChangeQuery(Guid modificationChangeId) : IRequest<ModificationChangeResponse>
{
    /// <summary>
    /// Gets or sets the unique identifier of the modification change to retrieve.
    /// </summary>
    public Guid ModificationChangeId { get; set; } = modificationChangeId;
}