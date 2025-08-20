using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.IrasService.Domain.Entities;
using Rsp.Logging.Interceptors;

namespace Rsp.IrasService.Application.Contracts.Services;

/// <summary>
/// Interface to create/read/update the application records in the database. Marked as IInterceptable to enable
/// the start/end logging for all methods.
/// </summary>
public interface IReviewBodyService : IInterceptable
{
    Task<AllReviewBodiesResponse> GetReviewBodies(int pageNumber, int pageSize, string sortField, string sortDirection, ReviewBodySearchRequest searchQuery);

    Task<ReviewBodyDto> GetReviewBody(Guid id);

    Task<ReviewBodyDto> CreateReviewBody(ReviewBodyDto reviewBody);

    Task<ReviewBodyDto> UpdateReviewBody(ReviewBodyDto reviewBody);

    Task<ReviewBodyDto?> DisableReviewBody(Guid id);

    Task<ReviewBodyDto?> EnableReviewBody(Guid id);

    Task<ReviewBodyUserDto?> AddUserToReviewBody(ReviewBodyUserDto reviewBodyUser);

    Task<ReviewBodyUserDto?> RemoveUserFromReviewBody(Guid reviewBodyId, Guid userId);

    Task<List<ReviewBodyUserDto>> GetRegulatoryBodiesUsersByUserId(Guid userId);
    Task<List<ReviewBodyUserDto>> GetRegulatoryBodiesUsersByIds(List<Guid> ids);
}