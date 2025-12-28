using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Infrastructure.EntitiesConfiguration;

public class ProjectModificationChangeAnswerConfiguration : IEntityTypeConfiguration<ProjectModificationChangeAnswer>
{
    public void Configure(EntityTypeBuilder<ProjectModificationChangeAnswer> builder)
    {
        builder.HasKey(ra => new { ra.ProjectModificationChangeId, ra.QuestionId, ra.CreatedBy });

        builder
            .HasOne(ra => ra.ProjectModificationChange)
            .WithMany()
            .HasForeignKey(r => r.ProjectModificationChangeId)
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