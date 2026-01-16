using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Infrastructure.EntitiesConfiguration;

public class ProjectRecordAnswerConfiguration : IEntityTypeConfiguration<ProjectRecordAnswer>
{
    public void Configure(EntityTypeBuilder<ProjectRecordAnswer> builder)
    {
        builder.HasKey(ra => new { ra.UserId, ra.QuestionId, ra.ProjectRecordId });

        builder
            .HasIndex(e => new { e.ProjectRecordId, e.QuestionId, e.UserId })
            .IncludeProperties(e => new { e.Response, e.SelectedOptions, e.OptionType, e.Category, e.Section, e.VersionId });

        builder
            .Property(e => e.VersionId)
            .HasDefaultValue("published version");
    }
}