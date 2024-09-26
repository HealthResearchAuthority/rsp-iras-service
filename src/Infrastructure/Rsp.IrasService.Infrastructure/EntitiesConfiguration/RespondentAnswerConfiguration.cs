using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rsp.QuestionSetService.Domain.Entities;

namespace Rsp.QuestionSetService.Infrastructure.EntitiesConfiguration;

public class RespondentAnswerConfiguration : IEntityTypeConfiguration<RespondentAnswer>
{
    public void Configure(EntityTypeBuilder<RespondentAnswer> builder)
    {
        builder.HasKey(ra => new { ra.RespondentId, ra.QuestionId, ra.ApplicationId });
    }
}