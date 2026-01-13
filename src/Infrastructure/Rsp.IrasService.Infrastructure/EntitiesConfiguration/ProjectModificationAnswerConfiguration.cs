using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Infrastructure.EntitiesConfiguration;

public class ProjectModificationAnswerConfiguration : IEntityTypeConfiguration<ProjectModificationAnswer>
{
    public void Configure(EntityTypeBuilder<ProjectModificationAnswer> builder)
    {
        builder.HasKey(ra => new { ra.ProjectModificationId, ra.QuestionId, ra.UserId });

        builder
            .HasOne(ra => ra.ProjectModification)
            .WithMany()
            .HasForeignKey(r => r.ProjectModificationId)
            .OnDelete(DeleteBehavior.Cascade);

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