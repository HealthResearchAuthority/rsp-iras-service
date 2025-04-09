using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Domain.Entities;
using Rsp.IrasService.Infrastructure.EntitiesConfiguration;

namespace Rsp.IrasService.Infrastructure;

public class IrasContext(DbContextOptions<IrasContext> options) : DbContext(options)
{
    public DbSet<ResearchApplication> ResearchApplications { get; set; }
    public DbSet<Respondent> Respondents { get; set; }
    public DbSet<RespondentAnswer> RespondentAnswers { get; set; }
    public DbSet<EventType> EventTypes { get; set; }
    public DbSet<EmailTemplate> EmailTemplates { get; set; }
    public DbSet<ReviewBody> ReviewBodies { get; set; }
    public DbSet<ReviewBodyAuditTrail> ReviewBodiesAuditTrails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ResearchApplicationConfiguration());
        modelBuilder.ApplyConfiguration(new RespondentConfiguration());
        modelBuilder.ApplyConfiguration(new RespondentAnswerConfiguration());
        modelBuilder.ApplyConfiguration(new EventTypeConfiguration());
        modelBuilder.ApplyConfiguration(new EmailTemplateConfiguration());
        modelBuilder.ApplyConfiguration(new ReviewBodyConfiguration());
        modelBuilder.ApplyConfiguration(new ReviewBodyAuditTrailConfiguration());
    }
}