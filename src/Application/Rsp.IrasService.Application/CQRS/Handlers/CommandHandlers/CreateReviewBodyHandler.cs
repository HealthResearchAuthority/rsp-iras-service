using MediatR;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.CQRS.Handlers.CommandHandlers;

public class CreateReviewBodyHandler(IReviewBodyService reviewBodyService)
    : IRequestHandler<CreateReviewBodyCommand, ReviewBodyDto>
{
    public async Task<ReviewBodyDto> Handle(CreateReviewBodyCommand request, CancellationToken cancellationToken)
    {
        return await reviewBodyService.CreateReviewBody(request.CreateReviewBodyRequest);
    }
}