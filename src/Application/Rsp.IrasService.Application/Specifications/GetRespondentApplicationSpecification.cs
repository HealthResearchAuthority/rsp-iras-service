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
    /// <param name="pageIndex">Page number (1-based). Must be greater than 0.</param>
    /// <param name="pageSize">Number of records per page. Must be greater than 0.</param>
    public GetRespondentApplicationSpecification(string respondentId, string? searchQuery, int pageIndex, int pageSize)
    {
        Query
         .AsNoTracking()
         .Where(entity => entity.ProjectPersonnelId == respondentId);

        if (!string.IsNullOrWhiteSpace(searchQuery))
        {
            var terms = searchQuery.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            foreach (var term in terms)
            {
                Query.Where(entity =>
                entity.Title.Contains(term) || entity.Description.Contains(term));
            }
        }

        if (pageIndex > 0 && pageSize > 0)
        {
            Query
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize);
        }
    }
}