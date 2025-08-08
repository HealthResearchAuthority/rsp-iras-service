using Ardalis.Specification;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Specifications;

public class GetRespondentProjectsTitlesSpecification : Specification<ProjectRecordAnswer>
{
    /// <summary>
    /// Defines a specification to return a paginated list of project records for a given respondent.
    /// </summary>
    /// <param name="respondentId">Unique Id of the respondent to get associated records for.</param>
    /// <param name="projectTitleQuestionId">Unique Id of question answer representing project title of project record.</param>
    public GetRespondentProjectsTitlesSpecification
    (
        string respondentId,
        string projectTitleQuestionId
    )
    {
        Query
            .AsNoTracking()
            .Where(entity =>
                entity.ProjectPersonnelId == respondentId &&
                entity.QuestionId == projectTitleQuestionId);
    }
}