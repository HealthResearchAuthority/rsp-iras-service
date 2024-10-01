using MediatR;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Queries;

public class GetApplicationsQuery : IRequest<IEnumerable<ApplicationResponse>>;