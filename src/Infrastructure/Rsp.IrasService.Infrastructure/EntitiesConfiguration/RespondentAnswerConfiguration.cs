using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Infrastructure.EntitiesConfiguration;

public class RespondentAnswerConfiguration : IEntityTypeConfiguration<RespondentAnswer>
{
    public void Configure(EntityTypeBuilder<RespondentAnswer> builder)
    {
        builder.HasKey(ra => new { ra.RespondentId, ra.QuestionId, ra.ApplicationId });
    }
}