using MediatR;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.Service.Application.CQRS.Handlers.CommandHandlers;

public class CreateReviewBodyHandler(IReviewBodyService reviewBodyService)
    : IRequestHandler<CreateReviewBodyCommand, ReviewBodyDto>
{
    public async Task<ReviewBodyDto> Handle(CreateReviewBodyCommand request, CancellationToken cancellationToken)
    {
        return await reviewBodyService.CreateReviewBody(request.CreateReviewBodyRequest);
    }
}