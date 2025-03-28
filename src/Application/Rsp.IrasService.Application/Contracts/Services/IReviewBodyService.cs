using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.Logging.Interceptors;

namespace Rsp.IrasService.Application.Contracts.Services;

/// <summary>
/// Interface to create/read/update the application records in the database. Marked as IInterceptable to enable
/// the start/end logging for all methods.
/// </summary>
public interface IReviewBodyService : IInterceptable
{
    Task<IEnumerable<ReviewBodyDto>> GetReviewBodies();
    Task<IEnumerable<ReviewBodyDto>> GetReviewBodies(Guid id);
    Task<ReviewBodyDto> CreateReviewBody(ReviewBodyDto reviewBody);
    Task<ReviewBodyDto> UpdateReviewBody(ReviewBodyDto reviewBody);
    Task<ReviewBodyDto?> DisableReviewBody(Guid id);
    Task<ReviewBodyDto?> EnableReviewBody(Guid id);
}