using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Infrastructure.EntitiesConfiguration;

public class ReviewBodyAuditTrailConfiguration : IEntityTypeConfiguration<ReviewBodyAuditTrail>
{
    public void Configure(EntityTypeBuilder<ReviewBodyAuditTrail> builder)
    {
        builder.HasKey(rb => rb.Id);

        builder
            .Property(rb => rb.Description)
            .IsRequired();

        builder
            .Property(x => x.User)
            .IsRequired()
            .HasMaxLength(250);

        builder
            .Property(x => x.DateTimeStamp)
            .IsRequired();

        builder
            .HasOne(x => x.ReviewBody)
            .WithMany()
            .HasForeignKey(x => x.ReviewBodyId);
    }
}