using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Infrastructure.EntitiesConfiguration;

public class ProjectRecordAnswerConfiguration : IEntityTypeConfiguration<ProjectRecordAnswer>
{
    public void Configure(EntityTypeBuilder<ProjectRecordAnswer> builder)
    {
        builder.HasKey(ra => new { ra.UserId, ra.QuestionId, ra.ProjectRecordId });

        builder
            .Property(e => e.VersionId)
            .HasDefaultValue("published version");
    }
}