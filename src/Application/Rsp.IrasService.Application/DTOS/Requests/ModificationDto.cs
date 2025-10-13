﻿namespace Rsp.IrasService.Application.DTOS.Requests;

public record ModificationDto
{
    public string Id { get; set; } = null!;
    public string ProjectRecordId { get; set; } = null!;
    public string ModificationId { get; set; } = null!;
    public string ShortProjectTitle { get; set; } = null!;
    public string ModificationType { get; set; } = null!;
    public string ChiefInvestigator { get; set; } = null!;
    public string LeadNation { get; set; } = null!;
    public string ParticipatingNation { get; set; } = null!;
    public string SponsorOrganisation { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public string Status { get; set; } = null!;
    public DateTime? SubmittedDate { get; set; }
}