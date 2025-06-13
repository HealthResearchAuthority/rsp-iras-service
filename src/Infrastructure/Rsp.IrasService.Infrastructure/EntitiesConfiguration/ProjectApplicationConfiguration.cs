using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Infrastructure.EntitiesConfiguration;

public class ProjectApplicationConfiguration : IEntityTypeConfiguration<ProjectApplication>
{
    public void Configure(EntityTypeBuilder<ProjectApplication> builder)
    {
        builder.HasKey(ra => ra.ProjectApplicationId);
    }
}