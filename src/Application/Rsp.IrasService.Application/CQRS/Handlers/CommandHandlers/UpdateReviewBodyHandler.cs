using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.CQRS.Handlers.CommandHandlers;

public class UpdateReviewBodyHandler(IReviewBodyService reviewBodyService)
    : IRequestHandler<UpdateReviewBodyCommand, ReviewBodyDto>
{
    public async Task<ReviewBodyDto> Handle(UpdateReviewBodyCommand request, CancellationToken cancellationToken)
    {
        return await reviewBodyService.UpdateReviewBody(request.UpdateReviewBodyRequest);
    }
}