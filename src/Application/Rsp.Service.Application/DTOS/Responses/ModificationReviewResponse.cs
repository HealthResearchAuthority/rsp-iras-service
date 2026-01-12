namespace Rsp.Service.Application.DTOS.Responses;

public record ModificationReviewResponse
{
    public Guid ModificationId { get; set; }
    public string? ReviewOutcome { get; set; }
    public string? Comment { get; set; }
    public string? ReasonNotApproved { get; set; }
}