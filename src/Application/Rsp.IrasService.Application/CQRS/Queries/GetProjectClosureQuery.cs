using MediatR;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Queries;

public class GetProjectClosureQuery(string projectRecordId) : BaseQuery, IRequest<ProjectClosureResponse>
{
    public string ProjectRecordId { get; } = projectRecordId;
}