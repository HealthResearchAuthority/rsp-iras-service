using MediatR;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Queries;

public class GetDocumentTypesQuery : IRequest<IEnumerable<DocumentTypeResponse>>;