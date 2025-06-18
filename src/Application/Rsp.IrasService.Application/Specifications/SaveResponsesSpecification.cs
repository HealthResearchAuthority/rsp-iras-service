using Ardalis.Specification;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Specifications;

public class SaveResponsesSpecification : Specification<ProjectApplicationRespondentAnswer>
{
    /// <summary>
    /// Defines a specification to return all records for the applicationId and categoryId
    /// </summary>
    /// <param name="applicationId">Unique Id of the application to get. Default: null for all records</param>
    /// <param name="respondentId">Category Id of the questions to be returned</param>
    public SaveResponsesSpecification(string applicationId, string respondentId)
    {
        Query
            .Where(entity => entity.ProjectApplicationId == applicationId && entity.Id == respondentId);
    }
}