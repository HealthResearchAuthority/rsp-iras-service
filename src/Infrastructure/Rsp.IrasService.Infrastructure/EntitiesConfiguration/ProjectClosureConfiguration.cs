using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Infrastructure.EntitiesConfiguration;

public class ProjectClosureConfiguration : IEntityTypeConfiguration<ProjectClosure>
{
    public void Configure(EntityTypeBuilder<ProjectClosure> builder)
    {
        builder.HasKey(ra => ra.Id);
    }
}