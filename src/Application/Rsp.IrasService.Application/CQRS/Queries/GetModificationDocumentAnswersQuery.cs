using MediatR;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.CQRS.Queries;

public class GetModificationDocumentAnswersQuery : IRequest<IEnumerable<ModificationDocumentAnswerDto>>
{
    public Guid Id { get; set; }
}