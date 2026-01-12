using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Infrastructure.EntitiesConfiguration;

public class ReviewBodyUserConfiguration : IEntityTypeConfiguration<RegulatoryBodyUser>
{
    public void Configure(EntityTypeBuilder<RegulatoryBodyUser> builder)
    {
        builder.HasKey(x => new { x.Id, x.UserId });

        builder.Property(rb => rb.Id)
            .IsRequired();

        builder.Property(rb => rb.UserId)
        .IsRequired();
    }
}