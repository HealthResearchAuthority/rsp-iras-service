using MediatR;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Commands;

public class UpdateProjectRecordStatusCommand(ApplicationRequest updateApplicationRequest) : IRequest<ApplicationResponse>
{
    public ApplicationRequest UpdateApplicationRequest { get; set; } = updateApplicationRequest;
}