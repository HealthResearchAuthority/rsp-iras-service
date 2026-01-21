using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Infrastructure.EntitiesConfiguration;

public class ProjectModificationConfiguration : IEntityTypeConfiguration<ProjectModification>
{
    public void Configure(EntityTypeBuilder<ProjectModification> builder)
    {
        builder.HasKey(r => r.Id);

        builder
            .HasIndex(e => new { e.ProjectRecordId, e.CreatedDate })
            .IsDescending(false, true)
            .IncludeProperties(e => new { e.Status });
    }
}