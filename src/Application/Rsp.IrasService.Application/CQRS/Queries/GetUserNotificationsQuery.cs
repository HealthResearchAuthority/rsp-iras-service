using MediatR;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.Service.Application.CQRS.Queries;

public class GetUserNotificationsQuery
(
    string userId,
    int pageNumber,
    int pageSize,
    string sortField,
    string sortDirection,
    string? type = null
) : IRequest<UserNotificationsResponse>
{
    public string UserId { get; set; } = userId;
    public string? Type { get; set; } = type;
    public int PageNumber { get; set; } = pageNumber;
    public int PageSize { get; set; } = pageSize;
    public string SortField { get; set; } = sortField;
    public string SortDirection { get; set; } = sortDirection;
}