using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Infrastructure.EntitiesConfiguration;

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