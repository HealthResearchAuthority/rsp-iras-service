using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Infrastructure.EntitiesConfiguration;

public class RespondentAnswerConfiguration : IEntityTypeConfiguration<RespondentAnswer>
{
    public void Configure(EntityTypeBuilder<RespondentAnswer> builder)
    {
        builder.HasKey(ra => new { ra.RespondentId, ra.QuestionId, ra.ApplicationId });

        builder
            .HasOne(ra => ra.Respondent)
            .WithMany()
            .HasForeignKey(r => r.RespondentId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(ra => ra.ResearchApplication)
            .WithMany()
            .HasForeignKey(r => r.ApplicationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}