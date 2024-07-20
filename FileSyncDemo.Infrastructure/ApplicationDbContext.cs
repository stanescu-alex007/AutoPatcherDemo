using Microsoft.EntityFrameworkCore;
using FileSyncDemo.Core.Entities;
using FileSyncDemo.Infrastructure.Configuration;

namespace FileSyncDemo.Infrastructure
{
    
    public class ApplicationDbContext : DbContext
    {
        public DbSet<SourceFile> SourceFiles { get; set; }
        public DbSet<ReplicaFile> ReplicaFiles{ get; set; }
        public DbSet<ActivityLog> ActivityLogs { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new SourceFileConfiguration());
            modelBuilder.ApplyConfiguration(new ReplicaFileConfiguration());
            modelBuilder.ApplyConfiguration(new ActivityLogConfiguration());
        }

    }
    
}
