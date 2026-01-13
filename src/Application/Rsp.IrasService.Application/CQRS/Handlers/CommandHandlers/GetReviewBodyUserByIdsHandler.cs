using MediatR;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.Service.Application.CQRS.Handlers.CommandHandlers;

public class GetReviewBodyUserByIdsHandler(IReviewBodyService reviewBodyService)
    : IRequestHandler<GetReviewBodyUserByIdsCommand, List<ReviewBodyUserDto>>
{
    public async Task<List<ReviewBodyUserDto>> Handle(GetReviewBodyUserByIdsCommand request,
        CancellationToken cancellationToken)
    {
        return await reviewBodyService.GetRegulatoryBodiesUsersByIds(request.Ids);
    }
}