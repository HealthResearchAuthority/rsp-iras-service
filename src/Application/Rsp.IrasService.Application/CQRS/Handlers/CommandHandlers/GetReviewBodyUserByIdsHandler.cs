using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.CQRS.Handlers.CommandHandlers;

public class GetReviewBodyUserByIdsHandler(IReviewBodyService reviewBodyService)
    : IRequestHandler<GetReviewBodyUserByIdsCommand, List<ReviewBodyUserDto>>
{
    public async Task<List<ReviewBodyUserDto>> Handle(GetReviewBodyUserByIdsCommand request,
        CancellationToken cancellationToken)
    {
        return await reviewBodyService.GetRegulatoryBodiesUsersByIds(request.Ids);
    }
}