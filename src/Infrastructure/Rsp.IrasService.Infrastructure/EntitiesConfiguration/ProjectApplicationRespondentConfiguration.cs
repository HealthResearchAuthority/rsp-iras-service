using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Infrastructure.EntitiesConfiguration;

public class ProjectApplicationRespondentConfiguration : IEntityTypeConfiguration<ProjectApplicationRespondent>
{
    public void Configure(EntityTypeBuilder<ProjectApplicationRespondent> builder)
    {
        builder.HasKey(r => r.Id);
    }
}