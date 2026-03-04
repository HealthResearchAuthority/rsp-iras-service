using Ardalis.Specification;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Application.Specifications;

public class SaveModificationResponsesSpecification : Specification<ProjectModificationAnswer>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SaveModificationResponsesSpecification"/> class.
    /// Defines a specification to return all <see cref="ProjectModificationAnswer"/> records
    /// for the specified modificationChangeId, projectRecordId, and userId.
    /// </summary>
    /// <param name="modificationId">Unique identifier of the project modification change.</param>
    /// <param name="projectRecordId">Unique identifier of the project record.</param>
    public SaveModificationResponsesSpecification(Guid modificationId, string projectRecordId)
    {
        Query
            .Where
            (
                entity =>
                entity.ProjectModificationId == modificationId &&
                entity.ProjectRecordId == projectRecordId
            );
    }
}