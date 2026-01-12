using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Infrastructure.EntitiesConfiguration;

public class ModificationParticipatingOrganisationConfiguration : IEntityTypeConfiguration<ModificationParticipatingOrganisation>
{
    public void Configure(EntityTypeBuilder<ModificationParticipatingOrganisation> builder)
    {
        builder.HasKey(r => r.Id);

        builder
            .HasOne(ra => ra.ProjectModificationChange)
            .WithMany()
            .HasForeignKey(r => r.ProjectModificationChangeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(ra => ra.ProjectRecord)
            .WithMany()
            .HasForeignKey(r => r.ProjectRecordId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}