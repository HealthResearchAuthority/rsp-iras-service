using MediatR;
using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.Service.Application.CQRS.Queries;

public class GetModificationParticipatingOrganisationsQuery : IRequest<IEnumerable<ModificationParticipatingOrganisationDto>>
{
    /// <summary>
    /// Gets or sets the project modification change identifier.
    /// </summary>
    public Guid ProjectModificationChangeId { get; set; }

    /// <summary>
    /// Gets or sets the project record identifier.
    /// </summary>
    public string ProjectRecordId { get; set; } = null!;

    /// <summary>
    /// Gets or sets the project personnel identifier.
    /// </summary>
    public string UserId { get; set; } = null!;
}