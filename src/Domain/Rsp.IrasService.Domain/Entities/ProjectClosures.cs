using Rsp.IrasService.Domain.Attributes;
using Rsp.IrasService.Domain.Interfaces;

namespace Rsp.IrasService.Domain.Entities;

/// <summary>
/// Represents a project closure records, including both the researcher’s submitted project closure request and the sponsor’s actions taken
/// </summary>
public class ProjectClosures : IAuditable
{
    /// <summary>
    /// Gets or sets the unique identifier for the project closure.
    /// </summary>
    public string TransactionId { get; set; } = null!;

    /// <summary>
    /// Gets or sets the identifier for the researcher associated with this projectrecord table.
    /// </summary>
    public string ProjectRecordId { get; set; } = null!;

    /// <summary>
    /// Gets or sets the identifier for researcher entered date from the screen i.e actual project closure date
    /// </summary>
    public DateTime ClosureDate { get; set; }

    /// <summary>
    /// Gets or sets the identifier for the researcher email.
    /// </summary>
    public string UserEmail { get; set; } = null!;

    /// <summary>
    /// Gets or sets the identifier for researcher when sent to sponsor for project closure.
    /// </summary>
    public DateTime SentToSponsorDate { get; set; }

    /// <summary>
    /// Gets or sets the identifier when sponsor takes action to authorise or not authorise.
    /// </summary>
    public DateTime DateActioned { get; set; }

    /// <summary>
    /// Gets or sets the title of the project.
    /// </summary>
    public string ShortProjectTitle { get; set; } = null!;

    /// <summary>
    /// Gets or sets the IRAS identifier associated with the project, if any.
    /// </summary>
    public int? IrasId { get; set; }

    /// <summary>
    /// Gets or sets the status of the project closure.
    /// </summary>
    [Auditable]
    public string? Status { get; set; }

    /// <summary>
    /// Gets or sets the user who created the project record.
    /// </summary>
    public string CreatedBy { get; set; } = null!;

    /// <summary>
    /// Gets or sets the user who last updated the project record.
    /// </summary>
    public string UpdatedBy { get; set; } = null!;

    // navigation properties
    public ProjectRecord ProjectRecord { get; set; } = null!;
}