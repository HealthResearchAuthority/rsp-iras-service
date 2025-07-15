using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rsp.IrasService.Domain.Entities;
using Rsp.IrasService.Infrastructure.Helpers;

namespace Rsp.IrasService.Infrastructure.EntitiesConfiguration;

public class ProjectRecordAnswerConfiguration : IEntityTypeConfiguration<ProjectRecordAnswer>
{
    public void Configure(EntityTypeBuilder<ProjectRecordAnswer> builder)
    {
        builder.HasKey(ra => new { ra.ProjectPersonnelId, ra.QuestionId, ra.ProjectRecordId });

        builder
            .HasOne(ra => ra.ProjectPersonnel)
            .WithMany()
            .HasForeignKey(r => r.ProjectPersonnelId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasData(JsonHelper.Parse<ProjectRecordAnswer>("ProjectRecordAnswers.json"));
    }
}