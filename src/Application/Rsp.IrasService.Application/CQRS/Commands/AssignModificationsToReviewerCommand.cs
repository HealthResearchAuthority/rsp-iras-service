using MediatR;

namespace Rsp.IrasService.Application.CQRS.Commands;

public class AssignModificationsToReviewerCommand(List<string> modificationIds, string reviewerId, string reivewerEmail) : IRequest
{
    public List<string> ModificationIds { get; set; } = modificationIds;
    public string ReviewerId { get; set; } = reviewerId;
    public string ReviewerEmail { get; set; } = reivewerEmail;
}