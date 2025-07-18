using Ardalis.Specification;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Specifications;

public class SaveModificationParticipatingOrganisationAnswerSpecification : Specification<ModificationParticipatingOrganisationAnswer>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SaveModificationParticipatingOrganisationAnswerSpecification"/> class.
    /// Defines a specification to return all <see cref="ModificationParticipatingOrganisationAnswer"/> records
    /// for the specified modificationParticipatingOrganisationId.
    /// </summary>
    /// <param name="modificationParticipatingOrganisationId">Unique identifier of the modification participating organisation.</param>
    public SaveModificationParticipatingOrganisationAnswerSpecification(Guid modificationParticipatingOrganisationId)
    {
        Query
            .Where
            (
                entity =>
                entity.ModificationParticipatingOrganisationId == modificationParticipatingOrganisationId
            );
    }
}