using Ardalis.Specification;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Application.Specifications;

public class GetRespondentAnswersSpecification : Specification<EffectiveProjectRecordAnswer>
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
            .Where(entity => entity.ProjectRecordId == applicationId && entity.Category == categoryId);
    }

    /// <summary>
    /// Defines a specification to return all records for the applicationId
    /// </summary>
    /// <param name="applicationId">Unique Id of the application to get</param>
    public GetRespondentAnswersSpecification(string applicationId)
    {
        Query
            .AsNoTracking()
            .Where(entity => entity.ProjectRecordId == applicationId);
    }
}