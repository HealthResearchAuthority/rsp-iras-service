using Ardalis.Specification;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Specifications;

public class GetRespondentApplicationSpecification : Specification<ResearchApplication>
{
    /// <summary>
    /// Defines a specification to return a single, all or a number of records
    /// </summary>
    /// <param name="respondentId">Unique Id of the respondent to get associated records for.</param>
    /// <param name="id">Unique Id of the application to get. Default: null for all records</param>
    /// <param name="records">Number of records to return. Default: 0 for all records</param>

    public GetRespondentApplicationSpecification(string respondentId, string? id = null, int records = 0)
    {
        Query
            .AsNoTracking()
            .Where(entity => entity.ApplicationId == id && entity.RespondentId == respondentId, id != null)
            .Where(entity => entity.RespondentId == respondentId, id == null)
            .Skip(records, id == null && records == 0)
            .Take(records, id == null && records != 0);
    }
}