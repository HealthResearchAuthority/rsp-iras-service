using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Infrastructure.EntitiesConfiguration;

public class ProjectApplicationRespondentAnswerConfiguration : IEntityTypeConfiguration<ProjectApplicationRespondentAnswer>
{
    public void Configure(EntityTypeBuilder<ProjectApplicationRespondentAnswer> builder)
    {
        builder.HasKey(ra => new { ra.Id, ra.QuestionId, ra.ProjectApplicationId });

        builder
            .HasOne(ra => ra.ProjectApplicationRespondent)
            .WithMany()
            .HasForeignKey(r => r.Id)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(ra => ra.ProjectApplication)
            .WithMany()
            .HasForeignKey(r => r.ProjectApplicationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}