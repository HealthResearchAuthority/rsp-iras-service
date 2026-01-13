using MediatR;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.Service.Application.CQRS.Handlers.CommandHandlers;

public class GetReviewBodyUserHandler(IReviewBodyService reviewBodyService)
    : IRequestHandler<GetReviewBodyUserCommand, List<ReviewBodyUserDto>>
{
    public async Task<List<ReviewBodyUserDto>> Handle(GetReviewBodyUserCommand request,
        CancellationToken cancellationToken)
    {
        return await reviewBodyService.GetRegulatoryBodiesUsersByUserId(request.UserId);
    }
}