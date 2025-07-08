using Ardalis.Specification;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Specifications;

public class SaveModificationResponsesSpecification : Specification<ProjectModificationAnswer>
{
    /// <summary>
    /// Defines a specification to return all records for the modificationChangeId, projectRecordId and projectPresonnelId
    /// </summary>
    /// <param name="projectRecordId">Unique Id of the project</param>
    /// <param name="projectPersonnelId">Project Personnel Id who submitted the change</param>
    public SaveModificationResponsesSpecification(Guid modificationChangeId, string projectRecordId, string projectPersonnelId)
    {
        Query
            .Where
            (
                entity =>
                entity.ProjectModificationChangeId == modificationChangeId &&
                entity.ProjectRecordId == projectRecordId &&
                entity.ProjectPersonnelId == projectPersonnelId
            );
    }
}