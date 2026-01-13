using MediatR;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Application.DTOS.Responses;

namespace Rsp.Service.Application.CQRS.Commands;

public class CreateApplicationCommand(ApplicationRequest createApplicationRequest) : IRequest<ApplicationResponse>
{
    public ApplicationRequest CreateApplicationRequest { get; set; } = createApplicationRequest;
}