using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Infrastructure.EntitiesConfiguration;

public class ProjectModificationConfiguration : IEntityTypeConfiguration<ProjectModification>
{
    public void Configure(EntityTypeBuilder<ProjectModification> builder)
    {
        builder.HasKey(r => r.Id);

        //builder
        //   .HasOne(ra => ra.ProjectRecord)
        //   .WithMany()
        //   .HasForeignKey(r => r.ProjectRecordId)
        //   .OnDelete(DeleteBehavior.Cascade);
    }
}