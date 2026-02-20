using Ardalis.Specification;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Application.Specifications;

public class SaveModificationChangeResponsesSpecification : Specification<ProjectModificationChangeAnswer>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SaveModificationChangeResponsesSpecification"/> class.
    /// Defines a specification to return all <see cref="ProjectModificationChangeAnswer"/> records
    /// for the specified modificationChangeId, projectRecordId, and userId.
    /// </summary>
    /// <param name="modificationChangeId">Unique identifier of the project modification change.</param>
    /// <param name="projectRecordId">Unique identifier of the project record.</param>
    public SaveModificationChangeResponsesSpecification(Guid modificationChangeId, string projectRecordId)
    {
        Query
            .Where
            (
                entity =>
                entity.ProjectModificationChangeId == modificationChangeId &&
                entity.ProjectRecordId == projectRecordId
            );
    }
}