using Ardalis.Specification;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Specifications;

/// <summary>
/// Specification for retrieving all <see cref="ProjectModificationChange"/> records associated
/// with a specific project modification.
/// </summary>
/// <remarks>
/// Applies a filter matching the provided <c>ProjectModificationId</c>. Use this specification when
/// you need to load all modification change entries linked to a single project modification aggregate.
/// </remarks>
public class GetModificationChangesSpecification : Specification<ProjectModificationChange>
{
    /// <summary>
    /// Creates a specification that filters <see cref="ModificationChangeResponse"/> items
    /// by the given project modification identifier.
    /// </summary>
    /// <param name="projectModificationId">The unique identifier of the parent project modification whose changes should be retrieved.</param>
    public GetModificationChangesSpecification(Guid projectModificationId)
    {
        Query.Where(entity => entity.ProjectModificationId == projectModificationId);
    }
}