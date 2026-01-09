using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Handlers.CommandHandlers;

public class UpdateProjectRecordStatusHandler(IApplicationsService applicationsService)
    : IRequestHandler<UpdateProjectRecordStatusCommand, ApplicationResponse>
{
    public async Task<ApplicationResponse> Handle(UpdateProjectRecordStatusCommand request, CancellationToken cancellationToken)
    {
        return await applicationsService.UpdateProjectRecordStatus(request.UpdateApplicationRequest);
    }
}