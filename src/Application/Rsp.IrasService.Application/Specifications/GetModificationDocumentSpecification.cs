using Ardalis.Specification;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Specifications;

public class GetModificationDocumentSpecification : Specification<ModificationDocument>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetModificationDocumentSpecification"/> class to return all documents for the specified modification change and project record.
    /// </summary>
    /// <param name="modificationId">Unique Id of the modification.</param>
    /// <param name="projectRecordId">Unique Id of the application to get. Default: null for all records.</param>
    /// <param name="projectPersonellId">Unique Id of the application to get. Default: null for all records.</param>
    public GetModificationDocumentSpecification(Guid modificationId, string projectRecordId, string projectPersonellId)
    {
        Query
            .AsNoTracking()
            .Where(entity =>
                entity.ProjectModificationId == modificationId);

        if (!string.IsNullOrEmpty(projectRecordId))
        {
            Query.Where(e => e.ProjectRecordId == projectRecordId);
        }

        if (!string.IsNullOrEmpty(projectPersonellId))
        {
            Query.Where(e => e.ProjectPersonnelId == projectPersonellId);
        }
    }
}