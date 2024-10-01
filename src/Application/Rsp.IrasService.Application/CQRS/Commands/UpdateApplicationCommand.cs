using MediatR;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Commands;

public class UpdateApplicationCommand(ApplicationRequest updateApplicationRequest) : IRequest<ApplicationResponse>
{
    public ApplicationRequest UpdateApplicationRequest { get; set; } = updateApplicationRequest;
}