using Ardalis.Specification;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Specifications;

public class GetApplicationSpecification : Specification<ProjectApplication>
{
    /// <summary>
    /// Defines a specification to return a single, all or a number of records
    /// </summary>
    /// <param name="id">Unique Id of the application to get. Default: null for all records</param>
    /// <param name="records">Number of records to return. Default: 0 for all records</param>
    public GetApplicationSpecification(string? id = null, int records = 0)
    {
        Query
            .AsNoTracking()
            .Where(entity => entity.Id == id, id != null)
            .Skip(records, id == null && records == 0)
            .Take(records, id == null && records != 0);
    }

    /// <summary>
    /// Defines a specification to return a single, all or a number of records with specified status
    /// </summary>
    /// <param name="status">Status of the application</param>
    /// <param name="id">Unique Id of the application to get. Default: null for all records</param>
    /// <param name="records">Number of records to return. Default: 0 for all records</param>
    public GetApplicationSpecification(string status, string? id = null, int records = 0)
    {
        Query
            .AsNoTracking()
            .Where(entity => entity.Id == id && entity.Status == status, id != null)
            .Where(entity => entity.Status == status, id == null)
            .Skip(records, id == null && records == 0)
            .Take(records, id == null && records != 0);
    }
}