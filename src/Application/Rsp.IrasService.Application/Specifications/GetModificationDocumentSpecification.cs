using Ardalis.Specification;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Specifications;

public class GetModificationDocumentSpecification : Specification<ModificationDocument>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetModificationDocumentSpecification"/> class to return all documents for the specified modification change and project record.
    /// </summary>
    /// <param name="modificationChangeId">Unique Id of the change added to the modification.</param>
    /// <param name="projectRecordId">Unique Id of the application to get. Default: null for all records.</param>
    /// <param name="projectPersonellId">Unique Id of the application to get. Default: null for all records.</param>
    public GetModificationDocumentSpecification(Guid modificationChangeId, string projectRecordId, string projectPersonellId)
    {
        Query
            .AsNoTracking()
            .Where(entity =>
                entity.ProjectModificationChangeId == modificationChangeId &&
                entity.ProjectRecordId == projectRecordId &&
                entity.ProjectPersonnelId == projectPersonellId
            );
    }
}