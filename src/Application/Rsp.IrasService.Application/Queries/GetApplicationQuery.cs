using MediatR;
using Rsp.IrasService.Application.Responses;

namespace Rsp.IrasService.Application.Queries;

public class GetApplicationQuery(string id) : IRequest<GetApplicationResponse>
{
    public string Id { get; } = id;
}