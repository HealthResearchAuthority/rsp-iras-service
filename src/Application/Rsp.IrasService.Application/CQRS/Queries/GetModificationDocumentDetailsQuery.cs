using MediatR;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.CQRS.Queries;

public class GetModificationDocumentDetailsQuery : IRequest<ModificationDocumentDto>
{
    public Guid Id { get; set; }
}