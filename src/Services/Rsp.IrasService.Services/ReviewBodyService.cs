using Azure;
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
}