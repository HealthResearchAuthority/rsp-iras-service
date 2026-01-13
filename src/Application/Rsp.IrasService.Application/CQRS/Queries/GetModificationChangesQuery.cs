using MediatR;
using Rsp.Service.Application.DTOS.Responses;

namespace Rsp.Service.Application.CQRS.Queries;

/// <summary>
/// Query to retrieve all modification changes associated with a specific project modification.
/// </summary>
/// <remarks>
/// This query returns zero or more <see cref="ModificationChangeResponse"/> records related
/// to the provided project modification identifier.
/// </remarks>
public class GetModificationChangesQuery : IRequest<IEnumerable<ModificationChangeResponse>>
{
    /// <summary>
    /// Gets or sets the unique identifier of the project record.
    /// </summary>
    public required string ProjectRecordId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the project modification whose changes are being retrieved.
    /// </summary>
    public required Guid ProjectModificationId { get; set; }
}