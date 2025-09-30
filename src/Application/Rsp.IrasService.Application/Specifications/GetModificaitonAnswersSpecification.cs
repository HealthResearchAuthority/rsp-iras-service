using Ardalis.Specification;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Specifications;

/// <summary>
/// Specification for retrieving <see cref="ProjectModificationAnswer"/> records based on modification change, project record, and optionally category.
/// </summary>
public class GetModificationAnswersSpecification : Specification<ProjectModificationAnswer>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetModificationAnswersSpecification"/> class to return all records for the specified modification change, project record, and category.
    /// </summary>
    /// <param name="modificationId">Unique Id of the change added to the modification.</param>
    /// <param name="projectRecordId">Unique Id of the application to get. Default: null for all records.</param>
    /// <param name="categoryId">Category Id of the questions to be returned.</param>
    public GetModificationAnswersSpecification(Guid modificationId, string projectRecordId, string categoryId)
    {
        Query
            .AsNoTracking()
            .Where(entity =>
                entity.ProjectModificationId == modificationId &&
                entity.ProjectRecordId == projectRecordId &&
                entity.Category == categoryId
            );
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetModificationAnswersSpecification"/> class to return all records for the specified modification change and project record.
    /// </summary>
    /// <param name="modificationId">Unique Id of the change added to the modification.</param>
    /// <param name="projectRecordId">Unique Id of the application to get.</param>
    public GetModificationAnswersSpecification(Guid modificationId, string projectRecordId)
    {
        Query
            .AsNoTracking()
            .Where(entity =>
                entity.ProjectModificationId == modificationId &&
                entity.ProjectRecordId == projectRecordId
            );
    }
}