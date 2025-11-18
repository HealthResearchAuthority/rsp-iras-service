using MediatR;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Queries;

public class GetModificationReviewResponsesQuery(Guid modificationId) : IRequest<ModificationReviewResponse>
{
    /// <summary>
    /// Gets or sets the unique identifier of the project modification.
    /// </summary>
    public Guid ProjectModificationId { get; } = modificationId;
}