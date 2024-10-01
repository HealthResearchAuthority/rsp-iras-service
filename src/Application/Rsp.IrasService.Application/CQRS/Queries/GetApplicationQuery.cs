using MediatR;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Queries;

public class GetApplicationQuery(string applicationId) : IRequest<ApplicationResponse>
{
    public string ApplicationId { get; } = applicationId;
}