using Ardalis.Specification;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Specifications;

public class SaveModificationDocumentsSpecification : Specification<ModificationDocument>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SaveModificationDocumentsSpecification"/> class.
    /// Defines a specification to return all <see cref="ModificationDocument"/> records for the specified project record, respondent and project modification change.
    /// </summary>
    /// <param name="projectModificationChangeId">Unique Id of the project modification change to get documents for. Default: null for all records.</param>
    /// <param name="projectRecordId">Unique Id of the project record to get answers for. Default: null for all records.</param>
    /// <param name="respondentId">Unique Id of the respondent (project personnel) whose answers are to be returned.</param>
    public SaveModificationDocumentsSpecification(Guid projectModificationChangeId, string projectRecordId, string respondentId)
    {
        Query
            .Where(entity => entity.ProjectModificationChangeId == projectModificationChangeId && entity.ProjectRecordId == projectRecordId && entity.ProjectPersonnelId == respondentId);
    }
}