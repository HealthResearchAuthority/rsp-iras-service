using MediatR;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.CQRS.Queries;

public class GetResponsesQuery : IRequest<IEnumerable<RespondentAnswerDto>>
{
    public string ApplicationId { get; set; } = null!;
    public string? CategoryId { get; set; }
}