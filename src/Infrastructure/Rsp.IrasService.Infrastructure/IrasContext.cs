﻿using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Domain.Entities;
using Rsp.IrasService.Infrastructure.EntitiesConfiguration;

namespace Rsp.IrasService.Infrastructure;

public class IrasContext(DbContextOptions<IrasContext> options) : DbContext(options)
{
    public DbSet<ProjectRecord> ProjectRecords { get; set; }
    public DbSet<ProjectPersonnel> ProjectPersonnels { get; set; }
    public DbSet<ProjectRecordAnswer> ProjectRecordAnswers { get; set; }
    public DbSet<EventType> EventTypes { get; set; }
    public DbSet<EmailTemplate> EmailTemplates { get; set; }
    public DbSet<RegulatoryBody> RegulatoryBodies { get; set; }
    public DbSet<RegulatoryBodyUser> RegulatoryBodiesUsers { get; set; }
    public DbSet<RegulatoryBodyAuditTrail> RegulatoryBodiesAuditTrail { get; set; }
    public DbSet<ProjectModification> ProjectModifications { get; set; }
    public DbSet<ProjectModificationChange> ProjectModificationChanges { get; set; }
    public DbSet<ProjectModificationAnswer> ProjectModificationAnswers { get; set; }
    public DbSet<DocumentType> DocumentTypes { get; set; }
    public DbSet<ModificationDocument> ModificationDocuments { get; set; }
    public DbSet<ModificationParticipatingOrganisation> ModificationParticipatingOrganisations { get; set; }
    public DbSet<ModificationParticipatingOrganisationAnswer> ModificationParticipatingOrganisationAnswers { get; set; }
    public DbSet<ModificationAreaOfChange> ModificationAreaOfChanges { get; set; }
    public DbSet<ModificationSpecificAreaOfChange> ModificationSpecificAreaOfChanges { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ProjectRecordConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectPersonnelConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectRecordAnswerConfiguration());
        modelBuilder.ApplyConfiguration(new EventTypeConfiguration());
        modelBuilder.ApplyConfiguration(new EmailTemplateConfiguration());
        modelBuilder.ApplyConfiguration(new RegulatoryBodyConfiguration());
        modelBuilder.ApplyConfiguration(new RegulatoryBodyAuditTrailConfiguration());
        modelBuilder.ApplyConfiguration(new ReviewBodyUserConfiguration());

        // project modifications entities configuration
        modelBuilder.ApplyConfiguration(new ProjectModificationConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectModificationChangeConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectModificationAnswerConfiguration());
        modelBuilder.ApplyConfiguration(new DocumentTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ModificationDocumentConfiguration());
        modelBuilder.ApplyConfiguration(new ModificationParticipatingOrganisationConfiguration());
        modelBuilder.ApplyConfiguration(new ModificationParticipatingOrganisationAnswerConfiguration());
        modelBuilder.ApplyConfiguration(new ModificationAreaOfChangeConfiguration());
        modelBuilder.ApplyConfiguration(new ModificationSpecificAreaOfChangeConfiguration());
    }
}