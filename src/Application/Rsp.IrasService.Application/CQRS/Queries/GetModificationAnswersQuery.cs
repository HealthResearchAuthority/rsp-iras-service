using MediatR;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.CQRS.Queries;

public class GetModificationAnswersQuery : IRequest<IEnumerable<RespondentAnswerDto>>
{
    public Guid ProjectModificationChangeId { get; set; }
    public string ProjectRecordId { get; set; } = null!;
    public string? CategoryId { get; set; }
}