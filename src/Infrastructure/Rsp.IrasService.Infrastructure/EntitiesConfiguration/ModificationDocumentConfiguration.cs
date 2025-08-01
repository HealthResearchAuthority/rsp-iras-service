﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Infrastructure.EntitiesConfiguration;

public class ModificationDocumentConfiguration : IEntityTypeConfiguration<ModificationDocument>
{
    public void Configure(EntityTypeBuilder<ModificationDocument> builder)
    {
        builder.HasKey(r => r.Id);

        builder
            .HasOne(ra => ra.ProjectModificationChange)
            .WithMany()
            .HasForeignKey(r => r.ProjectModificationChangeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(ra => ra.DocumentType)
            .WithMany()
            .HasForeignKey(r => r.DocumentTypeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(ra => ra.ProjectPersonnel)
            .WithMany()
            .HasForeignKey(r => r.ProjectPersonnelId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(ra => ra.ProjectRecord)
            .WithMany()
            .HasForeignKey(r => r.ProjectRecordId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}