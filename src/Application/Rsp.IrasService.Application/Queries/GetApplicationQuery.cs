using MediatR;
using Rsp.IrasService.Application.Responses;

namespace Rsp.IrasService.Application.Queries;

public class GetApplicationQuery(int id) : IRequest<GetApplicationResponse>
{
    public int Id { get; } = id;
}