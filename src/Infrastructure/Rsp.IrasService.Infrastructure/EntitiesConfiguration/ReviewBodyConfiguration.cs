using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Infrastructure.EntitiesConfiguration;

public class ReviewBodyConfiguration : IEntityTypeConfiguration<ReviewBody>
{
    public void Configure(EntityTypeBuilder<ReviewBody> builder)
    {
        builder.HasKey(rb => rb.Id);

        builder.Property(rb => rb.OrganisationName)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(rb => rb.EmailAddress)
            .IsRequired();
    }
}