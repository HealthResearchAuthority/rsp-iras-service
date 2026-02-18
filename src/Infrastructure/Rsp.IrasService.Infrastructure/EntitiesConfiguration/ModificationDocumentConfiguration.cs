using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Infrastructure.EntitiesConfiguration;

public class ModificationDocumentConfiguration : IEntityTypeConfiguration<ModificationDocument>
{
    public void Configure(EntityTypeBuilder<ModificationDocument> builder)
    {
        builder.HasKey(r => r.Id);

        builder
            .HasOne(ra => ra.ProjectModification)
            .WithMany()
            .HasForeignKey(r => r.ProjectModificationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(ra => ra.ProjectRecord)
            .WithMany()
            .HasForeignKey(r => r.ProjectRecordId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(d => d.ReplacedByDocument)
            .WithMany()
            .HasForeignKey(d => d.ReplacedByDocumentId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(d => d.LinkedDocument)
            .WithMany()
            .HasForeignKey(d => d.LinkedDocumentId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}