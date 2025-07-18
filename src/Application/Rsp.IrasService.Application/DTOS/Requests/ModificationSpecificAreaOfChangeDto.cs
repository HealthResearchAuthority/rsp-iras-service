﻿namespace Rsp.IrasService.Application.DTOS.Requests;

public class ModificationSpecificAreaOfChangeDto
{
    /// <summary>
    /// Gets or sets the unique identifier for this area of change.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the area of change.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the journey type for this modification specific area of change.
    /// </summary>
    public string? JourneyType { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for this modification specific area of change.
    /// </summary>
    public int ModificationAreaOfChangeId { get; set; }
}