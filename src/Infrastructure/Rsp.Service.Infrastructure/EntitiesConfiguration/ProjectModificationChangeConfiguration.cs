using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rsp.Service.Domain.Entities;
using Rsp.Service.Infrastructure.Helpers;

namespace Rsp.Service.Infrastructure.EntitiesConfiguration;

public class ProjectModificationChangeConfiguration : IEntityTypeConfiguration<ProjectModificationChange>
{
    public void Configure(EntityTypeBuilder<ProjectModificationChange> builder)
    {
        builder.HasKey(r => r.Id);
    }
}