using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Infrastructure.EntitiesConfiguration;

public class ProjectRecordAnswerConfiguration : IEntityTypeConfiguration<ProjectRecordAnswer>
{
    public void Configure(EntityTypeBuilder<ProjectRecordAnswer> builder)
    {
        builder.HasKey(ra => new { ra.CreatedBy, ra.QuestionId, ra.ProjectRecordId });

        builder
            .Property(e => e.VersionId)
            .HasDefaultValue("published version");
    }
}