using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Infrastructure.EntitiesConfiguration;

public class RegulatoryBodyAuditTrailConfiguration : IEntityTypeConfiguration<RegulatoryBodyAuditTrail>
{
    public void Configure(EntityTypeBuilder<RegulatoryBodyAuditTrail> builder)
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
            .HasOne(x => x.RegulatoryBody)
            .WithMany()
            .HasForeignKey(x => x.RegulatoryBodyId);
    }
}