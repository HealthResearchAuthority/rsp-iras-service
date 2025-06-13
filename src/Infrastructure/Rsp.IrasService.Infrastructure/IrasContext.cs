using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Domain.Entities;
using Rsp.IrasService.Infrastructure.EntitiesConfiguration;

namespace Rsp.IrasService.Infrastructure;

public class IrasContext(DbContextOptions<IrasContext> options) : DbContext(options)
{
    public DbSet<ProjectApplication> ProjectApplications { get; set; }
    public DbSet<ProjectApplicationRespondent> ProjectApplicationRespondents { get; set; }
    public DbSet<ProjectApplicationRespondentAnswer> ProjectApplicationRespondentAnswers { get; set; }
    public DbSet<EventType> EventTypes { get; set; }
    public DbSet<EmailTemplate> EmailTemplates { get; set; }
    public DbSet<RegulatoryBody> RegulatoryBodies { get; set; }
    public DbSet<RegulatoryBodyUsers> RegulatoryBodyUsers { get; set; }
    public DbSet<RegulatoryBodyAuditTrial> RegulatoryBodyAuditTrial { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ProjectApplicationConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectApplicationRespondentConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectApplicationRespondentAnswerConfiguration());
        modelBuilder.ApplyConfiguration(new EventTypeConfiguration());
        modelBuilder.ApplyConfiguration(new EmailTemplateConfiguration());
        modelBuilder.ApplyConfiguration(new RegulatoryBodyConfiguration());
        modelBuilder.ApplyConfiguration(new RegulatoryBodyAuditTrialConfiguration());
        modelBuilder.ApplyConfiguration(new ReviewBodyUsersConfiguration());
    }
}