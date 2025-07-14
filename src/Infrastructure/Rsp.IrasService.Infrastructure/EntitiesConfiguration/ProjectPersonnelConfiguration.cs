using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rsp.IrasService.Domain.Entities;
using Rsp.IrasService.Infrastructure.Helpers;

namespace Rsp.IrasService.Infrastructure.EntitiesConfiguration;

public class ProjectPersonnelConfiguration : IEntityTypeConfiguration<ProjectPersonnel>
{
    public void Configure(EntityTypeBuilder<ProjectPersonnel> builder)
    {
        builder.HasKey(r => r.Id);

        builder.HasData(JsonHelper.Parse<ProjectPersonnel>("ProjectPersonnel.json"));
    }
}