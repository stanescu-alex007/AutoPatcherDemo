using FileSyncDemo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileSyncDemo.Infrastructure.Configuration;

public class ActivityLogConfiguration : IEntityTypeConfiguration<ActivityLog>
{
    public void Configure(EntityTypeBuilder<ActivityLog> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Timestamp).IsRequired();
        builder.Property(x => x.ActionType).IsRequired();
        builder.Property(x => x.ExecutionType).IsRequired();
        builder.Property(x => x.Message).IsRequired();
        builder.Property(x => x.Status).IsRequired();
    }
}
