using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rsp.QuestionSetService.Domain.Entities;

namespace Rsp.QuestionSetService.Infrastructure.EntitiesConfiguration;

public class RespondentConfiguration : IEntityTypeConfiguration<Respondent>
{
    public void Configure(EntityTypeBuilder<Respondent> builder)
    {
        builder.HasKey(r => r.RespondentId);
    }
}