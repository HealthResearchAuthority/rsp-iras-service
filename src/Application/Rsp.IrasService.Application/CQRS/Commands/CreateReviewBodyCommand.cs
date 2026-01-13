using MediatR;
using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.Service.Application.CQRS.Commands;

public class CreateReviewBodyCommand(ReviewBodyDto reviewBodyDto) : IRequest<ReviewBodyDto>
{
    public ReviewBodyDto CreateReviewBodyRequest { get; set; } = reviewBodyDto;
}