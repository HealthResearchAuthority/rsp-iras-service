using MediatR;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.Application.DTOS.Responses;

namespace Rsp.Service.Application.CQRS.Handlers.CommandHandlers;

public class CreateApplicationHandler(IApplicationsService applicationsService)
    : IRequestHandler<CreateApplicationCommand, ApplicationResponse>
{
    public async Task<ApplicationResponse> Handle(CreateApplicationCommand request, CancellationToken cancellationToken)
    {
        return await applicationsService.CreateApplication(request.CreateApplicationRequest);
    }
}