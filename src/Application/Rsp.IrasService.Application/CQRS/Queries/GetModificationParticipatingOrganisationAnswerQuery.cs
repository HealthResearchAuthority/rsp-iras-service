using MediatR;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.CQRS.Queries;

public class GetModificationParticipatingOrganisationAnswerQuery : IRequest<ModificationParticipatingOrganisationAnswerDto>
{
    /// <summary>
    /// Gets or sets the participating organisation identifier.
    /// </summary>
    public Guid ModificationParticipatingOrganisationId { get; set; }
}