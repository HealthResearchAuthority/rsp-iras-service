namespace Rsp.IrasService.Application.DTOS.Requests;

public record ModificationReviewRequest
{
    public Guid ProjectModificationId { get; set; }
    public string Outcome { get; set; } = null!;
    public string? Comment { get; set; }
    public string? ReasonNotApproved { get; set; }
}