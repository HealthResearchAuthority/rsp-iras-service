using Ardalis.Specification;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Specifications;

public class DeleteModificationDocumentsSpecification : Specification<ModificationDocument>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SaveModificationDocumentsSpecification"/> class.
    /// Defines a specification to return all <see cref="ModificationDocument"/> records for the specified project record, respondent and project modification change.
    /// </summary>
    /// <param name="ids">Unique Ids of the project documents for. Default: null for all records.</param>
    public DeleteModificationDocumentsSpecification(IEnumerable<Guid> ids)
    {
        Query
            .Where(entity => ids.Contains(entity.Id));
    }
}