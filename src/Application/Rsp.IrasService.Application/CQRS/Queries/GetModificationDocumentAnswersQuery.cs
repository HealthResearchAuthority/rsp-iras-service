using MediatR;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.CQRS.Queries;

public class GetModificationDocumentAnswersQuery : IRequest<ModificationDocumentAnswerDto>
{
    public Guid Id { get; set; }
}