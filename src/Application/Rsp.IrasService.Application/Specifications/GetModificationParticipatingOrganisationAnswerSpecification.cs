using Ardalis.Specification;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Specifications;

public class GetModificationParticipatingOrganisationAnswerSpecification : Specification<ModificationParticipatingOrganisationAnswer>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetModificationParticipatingOrganisationAnswerSpecification"/> class to return the participating organisation answer for the specified participating organisation modification.
    /// </summary>
    /// <param name="participatingOrganisationId">Unique Id of the participating organisation added to the modification.</param>
    public GetModificationParticipatingOrganisationAnswerSpecification(Guid participatingOrganisationId)
    {
        Query
            .AsNoTracking()
            .Where(entity =>
                entity.ModificationParticipatingOrganisationId == participatingOrganisationId);
    }
}