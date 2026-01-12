using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Domain.Entities;
using Rsp.IrasService.Infrastructure.EntitiesConfiguration;

namespace Rsp.IrasService.Infrastructure;

public class IrasContext(DbContextOptions<IrasContext> options) : DbContext(options)
{
    public DbSet<ProjectRecord> ProjectRecords { get; set; }
    public DbSet<ProjectRecordAnswer> ProjectRecordAnswers { get; set; }
    public DbSet<EventType> EventTypes { get; set; }
    public DbSet<EmailTemplate> EmailTemplates { get; set; }
    public DbSet<RegulatoryBody> RegulatoryBodies { get; set; }
    public DbSet<RegulatoryBodyUser> RegulatoryBodiesUsers { get; set; }
    public DbSet<RegulatoryBodyAuditTrail> RegulatoryBodiesAuditTrail { get; set; }
    public DbSet<ProjectModification> ProjectModifications { get; set; }
    public DbSet<ProjectModificationChange> ProjectModificationChanges { get; set; }
    public DbSet<ProjectModificationAnswer> ProjectModificationAnswers { get; set; }
    public DbSet<ProjectModificationChangeAnswer> ProjectModificationChangeAnswers { get; set; }
    public DbSet<DocumentType> DocumentTypes { get; set; }
    public DbSet<ModificationDocument> ModificationDocuments { get; set; }
    public DbSet<ModificationDocumentAnswer> ModificationDocumentAnswers { get; set; }
    public DbSet<ModificationParticipatingOrganisation> ModificationParticipatingOrganisations { get; set; }
    public DbSet<ModificationParticipatingOrganisationAnswer> ModificationParticipatingOrganisationAnswers { get; set; }
    public DbSet<SponsorOrganisation> SponsorOrganisations { get; set; }
    public DbSet<SponsorOrganisationUser> SponsorOrganisationsUsers { get; set; }
    public DbSet<SponsorOrganisationAuditTrail> SponsorOrganisationsAuditTrail { get; set; }
    public DbSet<ProjectModificationAuditTrail> ProjectModificationAuditTrail { get; set; }
    public DbSet<ProjectRecordAuditTrail> ProjectRecordAuditTrail { get; set; }
    public DbSet<ProjectClosure> ProjectClosures { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ProjectRecordConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectRecordAnswerConfiguration());
        modelBuilder.ApplyConfiguration(new EventTypeConfiguration());
        modelBuilder.ApplyConfiguration(new EmailTemplateConfiguration());
        modelBuilder.ApplyConfiguration(new RegulatoryBodyConfiguration());
        modelBuilder.ApplyConfiguration(new RegulatoryBodyAuditTrailConfiguration());
        modelBuilder.ApplyConfiguration(new ReviewBodyUserConfiguration());
        modelBuilder.ApplyConfiguration(new SponsorOrganisationConfiguration());
        modelBuilder.ApplyConfiguration(new SponsorOrganisationUserConfiguration());
        modelBuilder.ApplyConfiguration(new SponsorOrganisationAuditTrailConfiguration());

        // project modifications entities configuration
        modelBuilder.ApplyConfiguration(new ProjectModificationConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectModificationChangeConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectModificationAnswerConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectModificationChangeAnswerConfiguration());
        modelBuilder.ApplyConfiguration(new DocumentTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ModificationDocumentConfiguration());
        modelBuilder.ApplyConfiguration(new ModificationDocumentAnswerConfiguration());
        modelBuilder.ApplyConfiguration(new ModificationParticipatingOrganisationConfiguration());
        modelBuilder.ApplyConfiguration(new ModificationParticipatingOrganisationAnswerConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectClosureConfiguration());
    }
}