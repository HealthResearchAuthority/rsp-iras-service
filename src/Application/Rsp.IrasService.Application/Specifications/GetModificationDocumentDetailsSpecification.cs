using Ardalis.Specification;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Application.Specifications;

public class GetModificationDocumentDetailsSpecification : Specification<ModificationDocument>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetModificationDocumentSpecification"/> class to return all documents for the specified modification change and project record.
    /// </summary>
    /// <param name="documentId">Unique Id of the change added to the modification.</param>
    public GetModificationDocumentDetailsSpecification(Guid documentId)
    {
        Query
            .AsNoTracking()
            .Where(entity =>
                entity.Id == documentId);
    }
}