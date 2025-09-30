using Ardalis.Specification;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Specifications;

/// <summary>
/// Specification for retrieving a single <see cref="ProjectModification"/> by its unique identifier.
/// </summary>
/// <remarks>
/// Applies a filter to match the provided modification Id. Intended for scenarios
/// where a specific modification record needs to be fetched.
/// </remarks>
public class GetModificationSpecification : Specification<ProjectModification>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetModificationSpecification"/> class.
    /// </summary>
    /// <param name="modificationId">The unique identifier of the modification change to retrieve.</param>
    public GetModificationSpecification(Guid modificationId)
    {
        Query.Where(entity => entity.Id == modificationId);
    }
}