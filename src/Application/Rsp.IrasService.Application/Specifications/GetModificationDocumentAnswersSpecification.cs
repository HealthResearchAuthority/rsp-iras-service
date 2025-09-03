using Ardalis.Specification;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Specifications;

public class GetModificationDocumentAnswersSpecification : Specification<ModificationDocumentAnswer>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetModificationDocumentAnswersSpecification"/> class to return all documents for the specified modification change and project record.
    /// </summary>
    /// <param name="documentId">Unique Id of the change added to the modification.</param>
    public GetModificationDocumentAnswersSpecification(Guid documentId)
    {
        Query
            .AsNoTracking()
            .Where(entity =>
                entity.ModificationDocumentId == documentId);
    }
}