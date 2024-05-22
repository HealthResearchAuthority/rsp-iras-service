﻿using MediatR;
using Microsoft.Extensions.Logging;
using Rsp.IrasService.Application.Contracts;
using Rsp.IrasService.Application.Queries;
using Rsp.IrasService.Application.Responses;

namespace Rsp.IrasService.Application.Handlers;

public class GetApplicationHandler(ILogger<GetApplicationHandler> logger, IApplicationsService applicationsService) : IRequestHandler<GetApplicationQuery, GetApplicationResponse>
{
    public async Task<GetApplicationResponse> Handle(GetApplicationQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting application with ID = {id}", request.Id);

        return await applicationsService.GetApplication(request.Id);
    }
}