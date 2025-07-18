using Ardalis.Specification;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Specifications;

public class GetDocumentTypesSpecification : Specification<DocumentType>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetDocumentTypesSpecification"/> class to return all document types.
    /// </summary>
    public GetDocumentTypesSpecification()
    {
        Query
            .AsNoTracking();
    }
}