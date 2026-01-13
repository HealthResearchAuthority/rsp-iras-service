using MediatR;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Application.DTOS.Responses;

namespace Rsp.Service.Application.CQRS.Queries;

public class GetDocumentsForModificationQuery : BaseQuery, IRequest<ProjectOverviewDocumentResponse>
{
    public required Guid ModificationId { get; set; }
    public required ProjectOverviewDocumentSearchRequest SearchQuery { get; set; }
    public required int PageNumber { get; set; }
    public required int PageSize { get; set; }
    public required string SortField { get; set; }
    public required string SortDirection { get; set; }
}