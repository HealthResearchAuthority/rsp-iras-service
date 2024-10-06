using Ardalis.Specification;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Specifications;

public class GetRespondentAnswersSpecification : Specification<RespondentAnswer>
{
    /// <summary>
    /// Defines a specification to return all records for the applicationId and categoryId
    /// </summary>
    /// <param name="applicationId">Unique Id of the application to get. Default: null for all records</param>
    /// <param name="categoryId">Category Id of the questions to be returned</param>
    public GetRespondentAnswersSpecification(string applicationId, string categoryId)
    {
        Query
            .AsNoTracking()
            .Where(entity => entity.ApplicationId == applicationId && entity.Category == categoryId);
    }

    /// <summary>
    /// Defines a specification to return all records for the applicationId
    /// </summary>
    /// <param name="applicationId">Unique Id of the application to get</param>
    public GetRespondentAnswersSpecification(string applicationId)
    {
        Query
            .AsNoTracking()
            .Where(entity => entity.ApplicationId == applicationId);
    }
}