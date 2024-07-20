using FileSyncDemo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileSyncDemo.Infrastructure.Configuration;

public class SourceFileConfiguration : IEntityTypeConfiguration<SourceFile>
{
    public void Configure(EntityTypeBuilder<SourceFile> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.FileName).IsRequired().HasMaxLength(30);
        builder.Property(x => x.FileExtension).IsRequired().HasDefaultValue("txt");
        builder.Property(x => x.FilePath).IsRequired();

    }
}
