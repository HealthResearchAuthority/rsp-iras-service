using MediatR;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.Application.DTOS.Responses;

namespace Rsp.Service.Application.CQRS.Handlers.CommandHandlers;

public class UpdateApplicationHandler(IApplicationsService applicationsService)
    : IRequestHandler<UpdateApplicationCommand, ApplicationResponse>
{
    public async Task<ApplicationResponse> Handle(UpdateApplicationCommand request, CancellationToken cancellationToken)
    {
        return await applicationsService.UpdateApplication(request.UpdateApplicationRequest);
    }
}