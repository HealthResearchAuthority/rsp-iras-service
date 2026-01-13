using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Infrastructure.EntitiesConfiguration;

public class RegulatoryBodyConfiguration : IEntityTypeConfiguration<RegulatoryBody>
{
    public void Configure(EntityTypeBuilder<RegulatoryBody> builder)
    {
        builder.HasKey(rb => rb.Id);

        builder.Property(rb => rb.RegulatoryBodyName)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(rb => rb.EmailAddress)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(rb => rb.CreatedBy)
            .HasMaxLength(250);

        builder.Property(rb => rb.UpdatedBy)
            .HasMaxLength(250);

        builder.Property(rb => rb.Countries)
            .HasMaxLength(500);

        builder.HasMany(x => x.Users)
            .WithOne()
            .HasForeignKey(x => x.Id);
    }
}