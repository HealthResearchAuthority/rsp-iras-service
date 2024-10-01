using MediatR;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Commands;

public class CreateApplicationCommand(ApplicationRequest createApplicationRequest) : IRequest<ApplicationResponse>
{
    public ApplicationRequest CreateApplicationRequest { get; set; } = createApplicationRequest;
}