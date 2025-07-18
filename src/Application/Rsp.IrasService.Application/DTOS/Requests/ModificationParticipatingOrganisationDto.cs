﻿namespace Rsp.IrasService.Application.DTOS.Requests;

/// <summary>
/// Request DTO representing a modification participating organisation.
/// </summary>
public class ModificationParticipatingOrganisationDto
{
    /// <summary>
    /// Gets or sets the unique identifier for the modification.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Gets or sets the identifier of the related project modification change.
    /// </summary>
    public Guid ProjectModificationChangeId { get; set; }

    /// <summary>
    /// Gets or sets the organisation Id.
    /// </summary>
    public string OrganisationId { get; set; } = null!;

    /// <summary>
    /// Gets or sets the project record identifier.
    /// </summary>
    public string ProjectRecordId { get; set; } = null!;

    /// <summary>
    /// Gets or sets the project personnel identifier.
    /// </summary>
    public string ProjectPersonnelId { get; set; } = null!;
}