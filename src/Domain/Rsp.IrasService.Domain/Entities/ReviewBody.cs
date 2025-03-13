﻿namespace Rsp.IrasService.Domain.Entities;

public class ReviewBody
{
    public Guid Id { get; set; }
    public string OrganisationName { get; set; }
    public string EmailAddress { get; set; }
    public List<string> Countries { get; set; } = new();
    public string Description { get; set; } = null!;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public string CreatedBy { get; set; } = null!;
    public string UpdatedBy { get; set; } = null!;
}