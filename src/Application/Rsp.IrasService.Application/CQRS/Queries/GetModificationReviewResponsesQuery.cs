using MediatR;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Queries;

public class GetModificationReviewResponsesQuery : IRequest<ModificationReviewResponse>
{
    /// <summary>
    /// Gets or sets the unique identifier of the project record.
    /// </summary>
    public required string ProjectRecordId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the project modification.
    /// </summary>
    public required Guid ProjectModificationId { get; set; }
}