using MediatR;
using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.Service.Application.CQRS.Commands;

public class UpdateReviewBodyCommand(ReviewBodyDto reviewBodyDto) : IRequest<ReviewBodyDto>
{
    public ReviewBodyDto UpdateReviewBodyRequest { get; set; } = reviewBodyDto;
}