using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rsp.IrasService.Domain.Entities;
using Rsp.IrasService.Infrastructure.Helpers;

namespace Rsp.IrasService.Infrastructure.EntitiesConfiguration;

public class ModificationSpecificAreaOfChangeConfiguration : IEntityTypeConfiguration<ModificationSpecificAreaOfChange>
{
    public void Configure(EntityTypeBuilder<ModificationSpecificAreaOfChange> builder)
    {
        builder.HasKey(r => r.Id);

        builder.HasData(JsonHelper.Parse<ModificationSpecificAreaOfChange>("ModificationSpecificAreaOfChanges.json"));
    }
}