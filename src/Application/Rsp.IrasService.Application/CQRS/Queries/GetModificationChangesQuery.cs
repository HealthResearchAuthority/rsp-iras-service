using MediatR;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Queries;

/// <summary>
/// Query to retrieve all modification changes associated with a specific project modification.
/// </summary>
/// <param name="projectModificationId">The unique identifier of the project modification whose changes are being requested.</param>
/// <remarks>
/// This query returns zero or more <see cref="ModificationChangeResponse"/> records related
/// to the provided project modification identifier.
/// </remarks>
public class GetModificationChangesQuery(Guid projectModificationId) : IRequest<IEnumerable<ModificationChangeResponse>>
{
    /// <summary>
    /// Gets or sets the unique identifier of the project modification whose changes are being retrieved.
    /// </summary>
    public Guid ProjectModificationId { get; set; } = projectModificationId;
}