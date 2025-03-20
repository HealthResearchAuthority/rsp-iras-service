using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Handlers.CommandHandlers;

public class UpdateApplicationHandler(IApplicationsService applicationsService)
    : IRequestHandler<UpdateApplicationCommand, ApplicationResponse>
{
    public async Task<ApplicationResponse> Handle(UpdateApplicationCommand request, CancellationToken cancellationToken)
    {
        return await applicationsService.UpdateApplication(request.UpdateApplicationRequest);
    }
}