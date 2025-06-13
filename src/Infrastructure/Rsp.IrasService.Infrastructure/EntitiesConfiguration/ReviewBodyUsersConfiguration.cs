using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Infrastructure.EntitiesConfiguration;

public class ReviewBodyUsersConfiguration : IEntityTypeConfiguration<ReviewBodyUsers>
{
    public void Configure(EntityTypeBuilder<ReviewBodyUsers> builder)
    {
        builder.HasKey(x => new { x.RegulatoryBodiesId, x.UserId });

        builder.Property(rb => rb.RegulatoryBodiesId)
            .IsRequired();

        builder.Property(rb => rb.UserId)
        .IsRequired();
    }
}