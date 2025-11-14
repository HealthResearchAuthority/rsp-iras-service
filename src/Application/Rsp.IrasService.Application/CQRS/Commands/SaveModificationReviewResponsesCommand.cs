using MediatR;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.CQRS.Commands;

public class SaveModificationReviewResponsesCommand(ModificationReviewRequest request) : IRequest
{
    public ModificationReviewRequest ModificationReviewRequest { get; set; } = request;
}