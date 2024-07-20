using FileSyncDemo.Core.Entities;
using FileSyncDemo.Core.Persistance;
using FileSyncDemo.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FileSyncDemo.Infrastructure;

public static class InfrastructureServices
{

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        => services
                .AddScoped(typeof(IRepository<>), typeof(Repository<>))
                .AddScoped<IRepository<SourceFile>, Repository<SourceFile>>()
                .AddScoped<IRepository<ReplicaFile>, Repository<ReplicaFile>>()
                .AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SyncYourFilesConnectionString"),
                sqlOptions => sqlOptions.MigrationsAssembly("FileSyncDemo.Infrastructure"));
            });
                

    
}
