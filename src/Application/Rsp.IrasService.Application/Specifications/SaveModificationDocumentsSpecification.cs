using Ardalis.Specification;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Specifications;

public class SaveModificationDocumentsSpecification : Specification<ModificationDocument>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SaveModificationDocumentsSpecification"/> class.
    /// Defines a specification to return all <see cref="ModificationDocument"/> records for the specified project record, respondent and project modification change.
    /// </summary>
    /// <param name="projectModificationId">Unique Id of the project modification to get documents for. Default: null for all records.</param>
    /// <param name="projectRecordId">Unique Id of the project record to get answers for. Default: null for all records.</param>
    /// <param name="respondentId">Unique Id of the respondent (project personnel) whose answers are to be returned.</param>
    public SaveModificationDocumentsSpecification(Guid projectModificationId, string projectRecordId, string respondentId)
    {
        Query
            .Where(entity => entity.ProjectModificationId == projectModificationId && entity.ProjectRecordId == projectRecordId && entity.UserId == respondentId);
    }
}