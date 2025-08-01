﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rsp.IrasService.Domain.Entities;
using Rsp.IrasService.Infrastructure.Helpers;

namespace Rsp.IrasService.Infrastructure.EntitiesConfiguration;

public class ModificationAreaOfChangeConfiguration : IEntityTypeConfiguration<ModificationAreaOfChange>
{
    public void Configure(EntityTypeBuilder<ModificationAreaOfChange> builder)
    {
        builder.HasKey(r => r.Id);

        builder.HasMany(x => x.ModificationSpecificAreaOfChanges)
            .WithOne()
            .HasForeignKey(x => x.ModificationAreaOfChangeId);

        builder.HasData(JsonHelper.Parse<ModificationAreaOfChange>("ModificationAreaOfChanges.json"));
    }
}