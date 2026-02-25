using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Infrastructure.EntitiesConfiguration;

public class UserNotificationConfiguration : IEntityTypeConfiguration<UserNotification>
{
    public void Configure(EntityTypeBuilder<UserNotification> builder)
    {
        builder.HasKey(un => un.Id);
        builder.Property(un => un.Type).HasMaxLength(50);
        builder.Property(un => un.RelatedEntityId).HasMaxLength(50);
        builder.Property(un => un.RelatedEntityType).HasMaxLength(50);
    }
}