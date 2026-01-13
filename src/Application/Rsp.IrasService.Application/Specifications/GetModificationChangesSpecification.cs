using Ardalis.Specification;
using Rsp.Service.Application.DTOS.Responses;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Application.Specifications;

/// <summary>
/// Specification for retrieving all <see cref="ProjectModification"/> records associated
/// with a specific project modification.
/// </summary>
/// <remarks>
/// Applies a filter matching the provided <c>ProjectModificationId</c>. Use this specification when
/// you need to load all modification change entries linked to a single project modification aggregate.
/// </remarks>
public class GetModificationChangesSpecification : Specification<ProjectModification>
{
    /// <summary>
    /// Creates a specification that filters <see cref="ModificationChangeResponse"/> items
    /// by the given project modification identifier.
    /// </summary>
    /// <param name="projectModificationId">The unique identifier of the parent project modification whose changes should be retrieved.</param>
    public GetModificationChangesSpecification(string projectRecordId, Guid projectModificationId)
    {
        Query.Where(entity => entity.Id == projectModificationId && entity.ProjectRecordId == projectRecordId);
    }
}