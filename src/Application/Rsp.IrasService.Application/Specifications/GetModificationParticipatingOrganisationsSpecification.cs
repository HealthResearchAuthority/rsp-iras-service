using Ardalis.Specification;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Specifications;

public class GetModificationParticipatingOrganisationsSpecification : Specification<ModificationParticipatingOrganisation>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetModificationParticipatingOrganisationsSpecification"/> class to return all documents for the specified modification change and project record.
    /// </summary>
    /// <param name="modificationChangeId">Unique Id of the change added to the modification.</param>
    /// <param name="projectRecordId">Unique Id of the application to get. Default: null for all records.</param>
    /// <param name="userId">Unique Id of the user to get. Default: null for all records.</param>
    public GetModificationParticipatingOrganisationsSpecification(Guid modificationChangeId, string projectRecordId, string userId)
    {
        Query
            .AsNoTracking()
            .Where(entity =>
                entity.ProjectModificationChangeId == modificationChangeId &&
                entity.ProjectRecordId == projectRecordId &&
                entity.UserId == userId
            );
    }
}