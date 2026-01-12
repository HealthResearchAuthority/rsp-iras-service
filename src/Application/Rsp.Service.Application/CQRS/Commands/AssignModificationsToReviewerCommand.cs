using MediatR;

namespace Rsp.Service.Application.CQRS.Commands;

public class AssignModificationsToReviewerCommand(List<string> modificationIds, string reviewerId, string reviewerEmail, string reviewerName) : IRequest
{
    public List<string> ModificationIds { get; set; } = modificationIds;
    public string ReviewerId { get; set; } = reviewerId;
    public string ReviewerEmail { get; set; } = reviewerEmail;
    public string ReviewerName { get; set; } = reviewerName;
}