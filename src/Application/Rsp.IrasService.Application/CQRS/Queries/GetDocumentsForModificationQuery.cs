using MediatR;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Queries;

public class GetDocumentsForModificationQuery
(
    Guid modificationId,
    ProjectOverviewDocumentSearchRequest searchQuery,
    int pageNumber,
    int pageSize,
    string sortField,
    string sortDirection
) : IRequest<ProjectOverviewDocumentResponse>
{
    public Guid ModificationId { get; } = modificationId;
    public ProjectOverviewDocumentSearchRequest SearchQuery { get; } = searchQuery;
    public int PageNumber { get; } = pageNumber;
    public int PageSize { get; } = pageSize;
    public string SortField { get; } = sortField;
    public string SortDirection { get; } = sortDirection;
}