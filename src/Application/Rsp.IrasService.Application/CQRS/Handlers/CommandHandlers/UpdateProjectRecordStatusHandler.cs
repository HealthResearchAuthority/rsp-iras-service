using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Commands;

namespace Rsp.IrasService.Application.CQRS.Handlers.CommandHandlers;

public class UpdateProjectRecordStatusHandler(IApplicationsService applicationsService)
    : IRequestHandler<UpdateProjectRecordStatusCommand>
{
    /// <summary>
    /// Processes the <see cref="UpdateProjectRecordStatusCommand"/> and updates the specified project record status.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task Handle(UpdateProjectRecordStatusCommand request, CancellationToken cancellationToken)
    {
        await applicationsService.UpdateProjectRecordStatus(request.ProjectRecordId, request.Status);
    }
}