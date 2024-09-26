using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rsp.QuestionSetService.Domain.Entities;

namespace Rsp.QuestionSetService.Infrastructure.EntitiesConfiguration;

public class ResearchApplicationConfiguration : IEntityTypeConfiguration<ResearchApplication>
{
    public void Configure(EntityTypeBuilder<ResearchApplication> builder)
    {
        builder.HasKey(ra => ra.ApplicationId);
    }
}