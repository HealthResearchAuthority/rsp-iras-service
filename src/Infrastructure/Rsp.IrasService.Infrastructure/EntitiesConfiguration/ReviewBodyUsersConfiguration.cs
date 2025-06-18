using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Infrastructure.EntitiesConfiguration;

public class ReviewBodyUsersConfiguration : IEntityTypeConfiguration<RegulatoryBodyUsers>
{
    public void Configure(EntityTypeBuilder<RegulatoryBodyUsers> builder)
    {
        builder.HasKey(x => new { x.Id, x.UserId });

        builder.Property(rb => rb.Id)
            .IsRequired();

        builder.Property(rb => rb.UserId)
        .IsRequired();
    }
}