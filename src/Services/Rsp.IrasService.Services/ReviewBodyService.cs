using Mapster;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.IrasService.Application.Specifications;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Services;

public class ReviewBodyService(IRegulatoryBodyRepository reviewBodyRepository) : IReviewBodyService
{
    public async Task<AllReviewBodiesResponse> GetReviewBodies(int pageNumber, int pageSize, string sortField, string sortDirection, ReviewBodySearchRequest searchQuery)
    {
        var specification = new GetReviewBodiesSpecification(pageNumber, pageSize, sortField, sortDirection, searchQuery);

        var rbResponses = await reviewBodyRepository.GetRegulatoryBodies(specification);
        var rbCount = await reviewBodyRepository.GetRegulatoryBodyCount(searchQuery);

        var response = new AllReviewBodiesResponse
        {
            ReviewBodies = rbResponses.Select(x => x.Adapt<ReviewBodyDto>()),
            TotalCount = rbCount
        };

        return response;
    }

    public async Task<ReviewBodyDto> GetReviewBody(Guid id)
    {
        var specification = new GetReviewBodySpecification(id: id);

        var response = await reviewBodyRepository.GetRegulatoryBody(specification);

        return response.Adapt<ReviewBodyDto>();
    }

    public async Task<ReviewBodyDto> CreateReviewBody(ReviewBodyDto reviewBody)
    {
        var reviewBodyEntity = reviewBody.Adapt<RegulatoryBody>();
        var response = await reviewBodyRepository.CreateRegulatoryBody(reviewBodyEntity);
        return response.Adapt<ReviewBodyDto>();
    }

    public async Task<ReviewBodyDto> UpdateReviewBody(ReviewBodyDto reviewBody)
    {
        var reviewBodyEntity = reviewBody.Adapt<RegulatoryBody>();
        var response = await reviewBodyRepository.UpdateRegulatoryBody(reviewBodyEntity);
        return response.Adapt<ReviewBodyDto>();
    }

    public async Task<ReviewBodyDto?> DisableReviewBody(Guid id)
    {
        var response = await reviewBodyRepository.DisableRegulatoryBody(id);
        return response.Adapt<ReviewBodyDto?>();
    }

    public async Task<ReviewBodyDto?> EnableReviewBody(Guid id)
    {
        var response = await reviewBodyRepository.EnableRegulatoryBody(id);
        return response.Adapt<ReviewBodyDto?>();
    }

    public async Task<ReviewBodyUserDto?> AddUserToReviewBody(ReviewBodyUserDto reviewBodyUser)
    {
        var reviewBodyUserEntity = reviewBodyUser.Adapt<RegulatoryBodyUser>();
        var response = await reviewBodyRepository.AddUserToRegulatoryBody(reviewBodyUserEntity);
        return response.Adapt<ReviewBodyUserDto?>();
    }

    public async Task<ReviewBodyUserDto?> RemoveUserFromReviewBody(Guid reviewBodyId, Guid userId)
    {
        var response = await reviewBodyRepository.RemoveUserFromRegulatoryBody(reviewBodyId, userId);
        return response.Adapt<ReviewBodyUserDto?>();
    }
}