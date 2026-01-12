using MediatR;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Application.DTOS.Responses;

namespace Rsp.Service.Application.CQRS.Commands;

public class UpdateProjectRecordStatusCommand(ApplicationRequest updateApplicationRequest) : IRequest<ApplicationResponse>
{
    public ApplicationRequest UpdateApplicationRequest { get; set; } = updateApplicationRequest;
}