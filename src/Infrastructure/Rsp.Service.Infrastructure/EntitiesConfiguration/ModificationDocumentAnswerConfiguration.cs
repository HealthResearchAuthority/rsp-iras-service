using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Infrastructure.EntitiesConfiguration;

public class ModificationDocumentAnswerConfiguration : IEntityTypeConfiguration<ModificationDocumentAnswer>
{
    public void Configure(EntityTypeBuilder<ModificationDocumentAnswer> builder)
    {
        builder.HasKey(r => r.Id);

        builder
            .HasOne(ra => ra.ModificationDocument)
            .WithMany()
            .HasForeignKey(r => r.ModificationDocumentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}