﻿using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Handlers.QueryHandlers;

public class GetApplicationWithStatusHandler(IApplicationsService applicationsService) : IRequestHandler<GetApplicationWithStatusQuery, ApplicationResponse>
{
    public async Task<ApplicationResponse> Handle(GetApplicationWithStatusQuery request, CancellationToken cancellationToken)
    {
        return await applicationsService.GetApplication(request.ApplicationId, request.ApplicationStatus);
    }
}