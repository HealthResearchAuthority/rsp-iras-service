﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Infrastructure.EntitiesConfiguration;

public class ProjectModificationChangeConfiguration : IEntityTypeConfiguration<ProjectModificationChange>
{
    public void Configure(EntityTypeBuilder<ProjectModificationChange> builder)
    {
        builder.HasKey(r => r.Id);
    }
}