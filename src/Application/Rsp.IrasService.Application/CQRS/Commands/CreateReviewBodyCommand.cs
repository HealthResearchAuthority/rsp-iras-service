using MediatR;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.Application.CQRS.Commands;

public class CreateReviewBodyCommand(ReviewBodyDto reviewBodyDto) : IRequest<ReviewBodyDto>
{
    public ReviewBodyDto CreateReviewBodyRequest { get; set; } = reviewBodyDto;
}