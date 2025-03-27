using MediatR;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.CQRS.Commands;

public class UpdateReviewBodyCommand(ReviewBodyDto reviewBodyDto) : IRequest<ReviewBodyDto>
{
    public ReviewBodyDto UpdateReviewBodyRequest { get; set; } = reviewBodyDto;
}