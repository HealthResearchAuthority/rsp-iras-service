using Ardalis.Specification;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Specifications;

public class SaveModificationResponsesSpecification : Specification<ProjectModificationAnswer>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SaveModificationResponsesSpecification"/> class.
    /// Defines a specification to return all <see cref="ProjectModificationAnswer"/> records
    /// for the specified modificationChangeId, projectRecordId, and userId.
    /// </summary>
    /// <param name="modificationId">Unique identifier of the project modification change.</param>
    /// <param name="projectRecordId">Unique identifier of the project record.</param>
    /// <param name="userId">Identifier of the project personnel who submitted the change.</param>
    public SaveModificationResponsesSpecification(Guid modificationId, string projectRecordId, string userId)
    {
        Query
            .Where
            (
                entity =>
                entity.ProjectModificationId == modificationId &&
                entity.ProjectRecordId == projectRecordId &&
                entity.UserId == userId
            );
    }
}