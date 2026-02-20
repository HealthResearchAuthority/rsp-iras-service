using System;
using Ardalis.Specification;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Application.Specifications;

public class GetModificationDocumentsByTypeSpecification : Specification<ModificationDocument>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetModificationDocumentsByTypeSpecification"/> class to return all documents for the specified modification change and project record.
    /// </summary>
    /// <param name="documentTypeId">Unique Id of the modification.</param>
    public GetModificationDocumentsByTypeSpecification(string documentTypeId)
    {
        Query
            .AsNoTracking();

        if (!string.IsNullOrWhiteSpace(documentTypeId))
        {
            Query.Where(document =>
                document.Id != Guid.Empty &&
                Context.ModificationDocumentAnswers.Any(answer =>
                    answer.ModificationDocumentId == document.Id &&
                    answer.SelectedOptions != null &&
                    answer.SelectedOptions.Contains(documentTypeId)
                ));
        }
    }
}