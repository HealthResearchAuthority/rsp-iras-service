using MediatR;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.Application.DTOS.Responses;

namespace Rsp.Service.Application.CQRS.Handlers.CommandHandlers;

public class UpdateProjectRecordStatusHandler(IApplicationsService applicationsService)
    : IRequestHandler<UpdateProjectRecordStatusCommand, ApplicationResponse>
{
    public async Task<ApplicationResponse> Handle(UpdateProjectRecordStatusCommand request, CancellationToken cancellationToken)
    {
        return await applicationsService.UpdateProjectRecordStatus(request.UpdateApplicationRequest);
    }
}