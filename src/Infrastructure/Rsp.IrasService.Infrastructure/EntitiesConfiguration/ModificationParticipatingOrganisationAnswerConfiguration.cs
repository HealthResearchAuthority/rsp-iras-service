using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Infrastructure.EntitiesConfiguration;

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
    }
}