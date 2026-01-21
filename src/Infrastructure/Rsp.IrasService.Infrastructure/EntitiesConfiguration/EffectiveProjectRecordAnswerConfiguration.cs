using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Infrastructure.EntitiesConfiguration;

public class EffectiveProjectRecordAnswerConfiguration : IEntityTypeConfiguration<EffectiveProjectRecordAnswer>
{
    public void Configure(EntityTypeBuilder<EffectiveProjectRecordAnswer> builder)
    {
        builder.HasNoKey();
        builder.ToView("vw_EffectiveProjectRecordAnswers");
    }
}