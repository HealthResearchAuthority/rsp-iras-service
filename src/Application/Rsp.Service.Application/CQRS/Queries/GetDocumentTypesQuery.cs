using MediatR;
using Rsp.Service.Application.DTOS.Responses;

namespace Rsp.Service.Application.CQRS.Queries;

public class GetDocumentTypesQuery : IRequest<IEnumerable<DocumentTypeResponse>>;