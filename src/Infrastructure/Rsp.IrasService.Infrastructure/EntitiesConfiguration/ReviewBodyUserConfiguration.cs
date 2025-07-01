using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Infrastructure.EntitiesConfiguration;

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