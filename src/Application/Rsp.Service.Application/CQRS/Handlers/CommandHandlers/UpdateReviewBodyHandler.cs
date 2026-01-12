using MediatR;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.Service.Application.CQRS.Handlers.CommandHandlers;

public class UpdateReviewBodyHandler(IReviewBodyService reviewBodyService)
    : IRequestHandler<UpdateReviewBodyCommand, ReviewBodyDto>
{
    public async Task<ReviewBodyDto> Handle(UpdateReviewBodyCommand request, CancellationToken cancellationToken)
    {
        return await reviewBodyService.UpdateReviewBody(request.UpdateReviewBodyRequest);
    }
}