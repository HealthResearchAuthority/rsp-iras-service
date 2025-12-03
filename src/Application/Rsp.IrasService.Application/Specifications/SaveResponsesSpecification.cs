using Ardalis.Specification;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Specifications;

/// <summary>
/// Specification for retrieving <see cref="ProjectRecordAnswer"/> entities
/// filtered by a specific project record and respondent (project personnel).
/// Used to fetch all answers associated with a given project record and respondent.
/// </summary>
public class SaveResponsesSpecification : Specification<ProjectRecordAnswer>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SaveResponsesSpecification"/> class.
    /// Defines a specification to return all <see cref="ProjectRecordAnswer"/> records for the specified project record and respondent.
    /// </summary>
    /// <param name="projectRecordId">Unique Id of the project record to get answers for. Default: null for all records.</param>
    /// <param name="respondentId">Unique Id of the respondent (project personnel) whose answers are to be returned.</param>
    public SaveResponsesSpecification(string projectRecordId, string respondentId)
    {
        Query
            .Where(entity => entity.ProjectRecordId == projectRecordId && entity.UserId == respondentId);
    }
}