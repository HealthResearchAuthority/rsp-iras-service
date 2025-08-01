﻿namespace Rsp.IrasService.Domain.Entities;

public class RegulatoryBodyAuditTrail
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid RegulatoryBodyId { get; set; }
    public DateTime DateTimeStamp { get; set; }
    public string Description { get; set; } = null!;
    public string User { get; set; } = null!;

    // navigation properties
    public RegulatoryBody RegulatoryBody { get; set; } = null!;
}