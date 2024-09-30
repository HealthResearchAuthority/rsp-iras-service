using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Infrastructure.EntitiesConfiguration;

public class ResearchApplicationConfiguration : IEntityTypeConfiguration<ResearchApplication>
{
    public void Configure(EntityTypeBuilder<ResearchApplication> builder)
    {
        builder.HasKey(ra => ra.ApplicationId);
    }
}