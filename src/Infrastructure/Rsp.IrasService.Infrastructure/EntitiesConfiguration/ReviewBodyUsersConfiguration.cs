using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Infrastructure.EntitiesConfiguration;

public class ReviewBodyUsersConfiguration : IEntityTypeConfiguration<ReviewBodyUsers>
{
    public void Configure(EntityTypeBuilder<ReviewBodyUsers> builder)
    {
        builder.HasKey(x => new { x.ReviewBodyId, x.UserId });

        builder.Property(rb => rb.ReviewBodyId)
            .IsRequired();

        builder.Property(rb => rb.UserId)
        .IsRequired();

        builder.HasOne(x => x.ReviewBody)
            .WithMany(x => x.Users)
            .HasForeignKey(x => x.ReviewBodyId);
    }
}