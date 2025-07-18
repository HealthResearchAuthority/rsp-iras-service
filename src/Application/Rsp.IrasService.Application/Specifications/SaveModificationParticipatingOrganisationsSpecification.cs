﻿using Ardalis.Specification;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Specifications;

public class SaveModificationParticipatingOrganisationsSpecification : Specification<ModificationParticipatingOrganisation>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SaveModificationParticipatingOrganisationsSpecification"/> class.
    /// Defines a specification to return all <see cref="ModificationParticipatingOrganisation"/> records
    /// for the specified modificationChangeId, projectRecordId, and projectPersonnelId.
    /// </summary>
    /// <param name="modificationChangeId">Unique identifier of the project modification change.</param>
    /// <param name="projectRecordId">Unique identifier of the project record.</param>
    /// <param name="projectPersonnelId">Identifier of the project personnel who submitted the change.</param>
    public SaveModificationParticipatingOrganisationsSpecification(Guid modificationChangeId, string projectRecordId, string projectPersonnelId)
    {
        Query
            .Where
            (
                entity =>
                entity.ProjectModificationChangeId == modificationChangeId &&
                entity.ProjectRecordId == projectRecordId &&
                entity.ProjectPersonnelId == projectPersonnelId
            );
    }
}