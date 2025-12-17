using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Infrastructure.EntitiesConfiguration;

public class ProjectClosureConfiguration : IEntityTypeConfiguration<ProjectClosures>
{
    public void Configure(EntityTypeBuilder<ProjectClosures> builder)
    {
        builder.HasKey(ra => ra.TransactionId);
    }
}