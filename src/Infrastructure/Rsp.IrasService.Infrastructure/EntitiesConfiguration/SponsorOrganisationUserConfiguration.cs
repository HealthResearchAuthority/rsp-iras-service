using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Infrastructure.EntitiesConfiguration;

public class SponsorOrganisationUserConfiguration : IEntityTypeConfiguration<SponsorOrganisationUser>
{
    public void Configure(EntityTypeBuilder<SponsorOrganisationUser> builder)
    {
        builder.HasKey(x => new { x.Id, x.UserId });

        builder.Property(rb => rb.Id)
            .IsRequired();

        builder.Property(rb => rb.UserId)
            .IsRequired();
    }
}