using Ardalis.Specification;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Specifications;

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
            .Where(entity => entity.Id == id && entity.ProjectPersonnelId == respondentId, id != null)
            .Where(entity => entity.ProjectPersonnelId == respondentId, id == null)
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

        builder.Where(entity => entity.ProjectPersonnelId == respondentId);

        if (!string.IsNullOrWhiteSpace(searchQuery.SearchTitleTerm))
        {
            var terms = searchQuery.SearchTitleTerm
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(t => t.ToLower());

            builder.Where(entity =>
                terms.All(term =>
                    (entity.Title != null && entity.Title.ToLower().Contains(term)) ||
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
                              entity.Status
                              .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                              .Any
                              (
                                  status => loweredStatus.Contains(status.ToLower())
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