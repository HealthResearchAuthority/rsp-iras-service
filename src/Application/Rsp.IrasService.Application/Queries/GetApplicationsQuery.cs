using MediatR;
using Rsp.IrasService.Application.Responses;

namespace Rsp.IrasService.Application.Queries;

public class GetApplicationsQuery : IRequest<IEnumerable<GetApplicationResponse>>;