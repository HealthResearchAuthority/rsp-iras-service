using MediatR;
using Rsp.Service.Application.DTOS.Responses;

namespace Rsp.Service.Application.CQRS.Queries;

public class GetProjectDocumentsAuditTrailQuery
(
    string projectRecordId,
    int pageNumber,
    int pageSize,
    string sortField,
    string sortDirection
) : BaseQuery, IRequest<ProjectDocumentsAuditTrailResponse>
{
    public string ProjectRecordId { get; } = projectRecordId;
    public int PageNumber { get; } = pageNumber;
    public int PageSize { get; } = pageSize;
    public string SortField { get; } = sortField;
    public string SortDirection { get; } = sortDirection;
}