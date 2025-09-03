using Ardalis.Specification;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Specifications;

public class SaveModificationDocumentAnswerSpecification : Specification<ModificationDocumentAnswer>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SaveModificationDocumentAnswerSpecification"/> class.
    /// Defines a specification to return all <see cref="ModificationDocumentAnswer"/> records
    /// for the specified modificationParticipatingOrganisationId.
    /// </summary>
    /// <param name="modificationDocumentId">Unique identifier of the modification participating organisation.</param>
    public SaveModificationDocumentAnswerSpecification()
    {
        Query
            .AsNoTracking();
    }
}