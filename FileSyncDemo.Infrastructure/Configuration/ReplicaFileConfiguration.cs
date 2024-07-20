using FileSyncDemo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileSyncDemo.Infrastructure.Configuration;

public class ReplicaFileConfiguration : IEntityTypeConfiguration<ReplicaFile>
{
    public void Configure(EntityTypeBuilder<ReplicaFile> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.FileName).IsRequired().HasMaxLength(30);
        builder.Property(x => x.FileExtension).IsRequired().HasDefaultValue("txt"); ;
        builder.Property(x => x.FilePath).IsRequired();

        builder.HasOne(x => x.SourceFile)
               .WithMany(x => x.ReplicaFiles)
               .HasForeignKey(x => x.SourceFileId);

        //If I delete the SourceFile(parent) all replicas are auto deleted even if I don t have .OnDelete(DeleteBehavior.Cascade);
        //I guess EF does it for me 
    }
}
