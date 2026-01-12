using MediatR;
using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.Service.Application.CQRS.Queries;

public class GetModificationParticipatingOrganisationAnswerQuery : IRequest<ModificationParticipatingOrganisationAnswerDto>
{
    /// <summary>
    /// Gets or sets the participating organisation identifier.
    /// </summary>
    public Guid ModificationParticipatingOrganisationId { get; set; }
}