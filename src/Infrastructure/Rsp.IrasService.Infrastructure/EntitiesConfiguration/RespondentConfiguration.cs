using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Infrastructure.EntitiesConfiguration;

public class RespondentConfiguration : IEntityTypeConfiguration<Respondent>
{
    public void Configure(EntityTypeBuilder<Respondent> builder)
    {
        builder.HasKey(r => r.RespondentId);
    }
}