using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Infrastructure.EntitiesConfiguration;

public class ProjectModificationAnswerConfiguration : IEntityTypeConfiguration<ProjectModificationAnswer>
{
    public void Configure(EntityTypeBuilder<ProjectModificationAnswer> builder)
    {
        builder.HasKey(ra => new { ra.ProjectModificationChangeId, ra.QuestionId, ra.ProjectPersonnelId });

        builder
            .HasOne(ra => ra.ProjectModificationChange)
            .WithMany()
            .HasForeignKey(r => r.ProjectModificationChangeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(ra => ra.ProjectPersonnel)
            .WithMany()
            .HasForeignKey(r => r.ProjectPersonnelId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(ra => ra.ProjectRecord)
            .WithMany()
            .HasForeignKey(r => r.ProjectRecordId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .Property(e => e.VersionId)
            .HasDefaultValue("published version");
    }
}