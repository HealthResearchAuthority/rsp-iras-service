using MediatR;
using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.Service.Application.CQRS.Queries;

public class GetModificationDocumentDetailsQuery : BaseQuery, IRequest<ModificationDocumentDto?>
{
    public Guid Id { get; set; }
}