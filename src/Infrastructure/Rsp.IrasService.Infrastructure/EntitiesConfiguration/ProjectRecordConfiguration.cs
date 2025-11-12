using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Infrastructure.EntitiesConfiguration;

public class ProjectRecordConfiguration : IEntityTypeConfiguration<ProjectRecord>
{
    public void Configure(EntityTypeBuilder<ProjectRecord> builder)
    {
        builder.HasKey(ra => ra.Id);
    }
}