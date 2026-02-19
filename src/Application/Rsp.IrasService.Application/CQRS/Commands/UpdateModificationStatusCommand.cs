using MediatR;

namespace Rsp.Service.Application.CQRS.Commands;

public class UpdateModificationStatusCommand : IRequest
{
    public string ProjectRecordId { get; set; }
    public Guid ProjectModificationId { get; set; }
    public string Status { get; set; }
    public string? RevisionDescription { get; set; }
    public string? ReasonNotApproved { get; set; }
    public string? ApplicantRevisionResponse { get; set; }
}