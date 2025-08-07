using Ardalis.Specification;
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
    /// <param name="searchQuery">Optional search query to filter projects by title or description.</param>
    /// <param name="sortField">Optional field name to sort the results by.</param>
    /// <param name="sortDirection">Optional sort direction: Ascending or Descending.</param>
    public GetRespondentApplicationSpecification
    (
        string respondentId,
        string? searchQuery,
        string? sortField,
        string? sortDirection
    )
    {
        var builder = Query
            .AsNoTracking();

        builder.Where(entity => entity.ProjectPersonnelId == respondentId);

        if (!string.IsNullOrWhiteSpace(searchQuery))
        {
            var terms = searchQuery.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            builder
                .Where
                (
                    entity => terms.Any
                    (
                        term => entity.Title.Contains(term)
                    )
                );
        }

        // Apply sorting
        switch ((sortField?.ToLower(), sortDirection?.ToLower()))
        {
            case ("title", "asc"):
                builder.OrderBy(x => x.Title);
                break;

            case ("title", "desc"):
                builder.OrderByDescending(x => x.Title);
                break;

            case ("status", "asc"):
                builder.OrderBy(x => x.Status);
                break;

            case ("status", "desc"):
                builder.OrderByDescending(x => x.Status);
                break;

            case ("createddate", "asc"):
                builder.OrderBy(x => x.CreatedDate);
                break;

            case ("createddate", "desc"):
                builder.OrderByDescending(x => x.CreatedDate);
                break;

            case ("irasid", "asc"):
                builder.OrderBy(x => x.IrasId);
                break;

            case ("irasid", "desc"):
                builder.OrderByDescending(x => x.IrasId);
                break;

            default:
                builder.OrderByDescending(x => x.CreatedDate);
                break;
        }
    }
}