using MediatR;
using Rsp.Service.Application.DTOS.Responses;

namespace Rsp.Service.Application.CQRS.Queries;

/// <summary>
/// Query to retrieve a modification change by its unique identifier.
/// </summary>
public class GetModificationChangeQuery : IRequest<ModificationChangeResponse>
{
    /// <summary>
    /// Gets or sets the unique identifier of the modification change to retrieve.
    /// </summary>
    public required Guid ModificationChangeId { get; set; }
}