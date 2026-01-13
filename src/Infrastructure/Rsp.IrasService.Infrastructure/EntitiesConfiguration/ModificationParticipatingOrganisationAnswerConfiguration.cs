using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Infrastructure.EntitiesConfiguration;

public class ModificationParticipatingOrganisationAnswerConfiguration : IEntityTypeConfiguration<ModificationParticipatingOrganisationAnswer>
{
    public void Configure(EntityTypeBuilder<ModificationParticipatingOrganisationAnswer> builder)
    {
        builder.HasKey(r => r.Id);

        builder
            .HasOne(ra => ra.ModificationParticipatingOrganisation)
            .WithMany()
            .HasForeignKey(r => r.ModificationParticipatingOrganisationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .Property(e => e.VersionId)
            .HasDefaultValue("published version");
    }
}