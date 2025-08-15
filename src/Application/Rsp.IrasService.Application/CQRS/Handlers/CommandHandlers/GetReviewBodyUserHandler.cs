using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.CQRS.Handlers.CommandHandlers;

public class GetReviewBodyUserHandler(IReviewBodyService reviewBodyService)
    : IRequestHandler<GetReviewBodyUserCommand, List<ReviewBodyUserDto>>
{
    public async Task<List<ReviewBodyUserDto>> Handle(GetReviewBodyUserCommand request,
        CancellationToken cancellationToken)
    {
        return await reviewBodyService.GetRegulatoryBodiesUsersByUserId(request.UserId);
    }
}