using MediatR;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.CQRS.Queries;

public class GetModificationDocumentDetailsQuery : BaseQuery, IRequest<ModificationDocumentDto?>
{
    public Guid Id { get; set; }
}