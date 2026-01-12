using MediatR;
using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.Service.Application.CQRS.Commands;

public class SaveModificationReviewResponsesCommand(ModificationReviewRequest request) : IRequest
{
    public ModificationReviewRequest ModificationReviewRequest { get; set; } = request;
}