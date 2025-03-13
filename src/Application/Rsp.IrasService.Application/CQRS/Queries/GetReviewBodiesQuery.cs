using MediatR;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.CQRS.Queries;

public class GetReviewBodiesQuery : IRequest<IEnumerable<ReviewBodyDto>>;