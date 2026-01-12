using MediatR;
using Rsp.Service.Application.DTOS.Responses;

namespace Rsp.Service.Application.CQRS.Queries;

public class GetApplicationQuery(string applicationId) : BaseQuery, IRequest<ApplicationResponse>
{
    public string ApplicationId { get; } = applicationId;
}