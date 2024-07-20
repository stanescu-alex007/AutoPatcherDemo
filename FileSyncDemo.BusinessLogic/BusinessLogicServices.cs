using FileSyncDemo.BusinessLogic.BackgroundServices;
using FileSyncDemo.BusinessLogic.Services.Implementations;
using FileSyncDemo.BusinessLogic.Services.Implementations.LogImplementations;
using FileSyncDemo.BusinessLogic.Services.Interfaces;
using FileSyncDemo.BusinessLogic.Services.Interfaces.LogInterfaces;
using FileSyncDemo.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FileSyncDemo.BusinessLogic;

public static class BusinessLogicServices
{

    public static IServiceCollection AddBusinessLogicServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ISourceFileService, SourceFileService>()
                .AddScoped<IReplicaFileService, ReplicaFileService>()
                .AddScoped<ISyncService, SyncService>()
                .AddScoped<IActivityLogService, ActivityLogService>()
                .AddScoped<ILogForCreateService, LogForCreateService>()
                .AddScoped<ILogForDeleteService, LogForDeleteService>()
                .AddScoped<ILogForSyncService, LogForSyncService>()

                .AddSingleton<FileSyncBackgroundService>()
                .AddHostedService<FileSyncBackgroundService>()

                .AddSingleton<FileCheckerBackgroundService>()
                .AddHostedService<FileCheckerBackgroundService>();




        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
                .AddInfrastructureServices(configuration);

        return services;
    }

}
