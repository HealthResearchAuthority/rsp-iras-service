using MediatR;
using Rsp.Service.Application.DTOS.Responses;

namespace Rsp.Service.Application.CQRS.Queries;

public class GetProjectClosureQuery(string projectRecordId) : BaseQuery, IRequest<ProjectClosuresSearchResponse>
{
    public string ProjectRecordId { get; } = projectRecordId;
}