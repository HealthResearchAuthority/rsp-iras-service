using MediatR;
using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.Service.Application.CQRS.Queries;

public class GetResponsesQuery : IRequest<IEnumerable<RespondentAnswerDto>>
{
    public string ApplicationId { get; set; } = null!;
    public string? CategoryId { get; set; }
}