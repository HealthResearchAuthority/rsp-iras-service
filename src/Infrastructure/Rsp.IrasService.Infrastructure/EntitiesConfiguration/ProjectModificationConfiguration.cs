using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rsp.IrasService.Domain.Entities;
using Rsp.IrasService.Infrastructure.Helpers;

namespace Rsp.IrasService.Infrastructure.EntitiesConfiguration;

public class ProjectModificationConfiguration : IEntityTypeConfiguration<ProjectModification>
{
    public void Configure(EntityTypeBuilder<ProjectModification> builder)
    {
        builder.HasKey(r => r.Id);

        builder.HasData(JsonHelper.Parse<ProjectModification>("ProjectModifications.json"));
    }
}