using Mapster;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.Specifications;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Services;

public class ReviewBodyService(IReviewBodyRepository reviewBodyRepository) : IReviewBodyService
{
    public async Task<IEnumerable<ReviewBodyDto>> GetReviewBodies()
    {
        var specification = new GetReviewBodySpecification();

        var responses = await reviewBodyRepository.GetReviewBodies(specification);

        return responses.Adapt<IEnumerable<ReviewBodyDto>>();
    }

    public async Task<IEnumerable<ReviewBodyDto>> GetReviewBodies(Guid id)
    {
        var specification = new GetReviewBodySpecification(id: id);

        var response = await reviewBodyRepository.GetReviewBodies(specification);

        return response.Adapt<IEnumerable<ReviewBodyDto>>();
    }

    public async Task<ReviewBodyDto> CreateReviewBody(ReviewBodyDto reviewBody)
    {
        var reviewBodyEntity = reviewBody.Adapt<ReviewBody>();
        var response = await reviewBodyRepository.CreateReviewBody(reviewBodyEntity);
        return response.Adapt<ReviewBodyDto>();
    }

    public async Task<ReviewBodyDto> UpdateReviewBody(ReviewBodyDto reviewBody)
    {
        var reviewBodyEntity = reviewBody.Adapt<ReviewBody>();
        var response = await reviewBodyRepository.UpdateReviewBody(reviewBodyEntity);
        return response.Adapt<ReviewBodyDto>();
    }

    public async Task<ReviewBodyDto?> DisableReviewBody(Guid id)
    {
        var response = await reviewBodyRepository.DisableReviewBody(id);
        return response.Adapt<ReviewBodyDto?>();
    }

    public async Task<ReviewBodyDto?> EnableReviewBody(Guid id)
    {
        var response = await reviewBodyRepository.EnableReviewBody(id);
        return response.Adapt<ReviewBodyDto?>();
    }

    public async Task<ReviewBodyUserDto?> AddUserToReviewBody(ReviewBodyUserDto reviewBodyUser)
    {
        var reviewBodyUserEntity = reviewBodyUser.Adapt<ReviewBodyUsers>();
        var response = await reviewBodyRepository.AddUserToReviewBody(reviewBodyUserEntity);
        return response.Adapt<ReviewBodyUserDto?>();
    }

    public async Task<ReviewBodyUserDto?> RemoveUserFromReviewBody(Guid reviewBodyId, Guid userId)
    {
        var response = await reviewBodyRepository.RemoveUserFromReviewBody(reviewBodyId, userId);
        return response.Adapt<ReviewBodyUserDto?>();
    }
}