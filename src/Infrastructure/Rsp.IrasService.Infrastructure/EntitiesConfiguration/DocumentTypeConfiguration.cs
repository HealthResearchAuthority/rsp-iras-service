using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Infrastructure.EntitiesConfiguration;

public class DocumentTypeConfiguration : IEntityTypeConfiguration<DocumentType>
{
    public void Configure(EntityTypeBuilder<DocumentType> builder)
    {
        builder.HasKey(r => r.Id);
    }
}