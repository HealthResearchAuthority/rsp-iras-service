using MediatR;
using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.Service.Application.CQRS.Queries;

public class GetModificationDocumentAnswersQuery : IRequest<IEnumerable<ModificationDocumentAnswerDto>>
{
    public Guid Id { get; set; }
}