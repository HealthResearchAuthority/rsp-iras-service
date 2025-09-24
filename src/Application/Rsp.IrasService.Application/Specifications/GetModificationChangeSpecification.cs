using Ardalis.Specification;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Specifications;

/// <summary>
/// Specification for retrieving a single <see cref="ProjectModificationChange"/> by its unique identifier.
/// </summary>
/// <remarks>
/// Applies a filter to match the provided modification change Id. Intended for scenarios
/// where a specific modification change record needs to be fetched.
/// </remarks>
public class GetModificationChangeSpecification : Specification<ProjectModificationChange>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetModificationChangeSpecification"/> class.
    /// </summary>
    /// <param name="modificationChangeId">The unique identifier of the modification change to retrieve.</param>
    public GetModificationChangeSpecification(Guid modificationChangeId)
    {
        Query.Where(entity => entity.Id == modificationChangeId);
    }
}