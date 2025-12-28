using Ardalis.Specification;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Application.Specifications;

public class GetRespondentApplicationSpecification : Specification<ProjectRecord>
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
            .Where(entity => entity.Id == id && entity.CreatedBy == respondentId, id != null)
            .Where(entity => entity.CreatedBy == respondentId, id == null)
            .Skip(records, id == null && records == 0)
            .Take(records, id == null && records != 0);
    }

    /// <summary>
    /// Defines a specification to return a paginated list of project records for a given respondent.
    /// </summary>
    /// <param name="respondentId">Unique Id of the respondent to get associated records for.</param>
    /// <param name="searchQuery">Optional search query to filter projects.</param>
    public GetRespondentApplicationSpecification
    (
        string respondentId,
        ApplicationSearchRequest searchQuery
    )
    {
        var fromDate = searchQuery.FromDate?.Date;
        var toDate = searchQuery.ToDate?.Date;
        var loweredStatus = searchQuery.Status
                .Select(s => s.ToLower())
                .ToList();

        var builder = Query
            .AsNoTracking();

        builder.Where(entity => entity.CreatedBy == respondentId);

        if (!string.IsNullOrWhiteSpace(searchQuery.SearchTitleTerm))
        {
            var terms = searchQuery.SearchTitleTerm
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(t => t.ToLower());

            builder.Where(entity =>
                terms.All(term =>
                    (entity.ShortProjectTitle != null && entity.ShortProjectTitle.ToLower().Contains(term)) ||
                    (entity.IrasId != null && entity.IrasId.Value.ToString().ToLower().Contains(term))
                )
            );
        }

        if (searchQuery.Status.Count != 0)
        {
            builder
                .Where
                (
                    entity => !string.IsNullOrWhiteSpace(entity.Status) &&
                        loweredStatus
                        .Any
                        (
                            status => entity.Status.ToLower().Contains(status)
                        )
                );
        }

        // ✅ Date-only filtering (ignore time)
        if (fromDate.HasValue)
        {
            builder.Where(entity => entity.CreatedDate.Date >= fromDate.Value);
        }

        if (toDate.HasValue)
        {
            builder.Where(entity => entity.CreatedDate.Date <= toDate.Value);
        }
    }
}