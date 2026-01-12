using MediatR;
using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.Service.Application.CQRS.Commands;

public class EnableSponsorOrganisationUserCommand(string rtsId, Guid userId) : IRequest<SponsorOrganisationUserDto>
{
    public string RtsId { get; set; } = rtsId;
    public Guid UserId { get; set; } = userId;
}