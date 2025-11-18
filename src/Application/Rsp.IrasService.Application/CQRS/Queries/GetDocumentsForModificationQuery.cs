using MediatR;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Queries;

public class GetDocumentsForModificationQuery : IRequest<ProjectOverviewDocumentResponse>
{
    public required Guid ModificationId { get; set; }
    public required ProjectOverviewDocumentSearchRequest SearchQuery { get; set; }
    public required int PageNumber { get; set; }
    public required int PageSize { get; set; }
    public required string SortField { get; set; }
    public required string SortDirection { get; set; }
}