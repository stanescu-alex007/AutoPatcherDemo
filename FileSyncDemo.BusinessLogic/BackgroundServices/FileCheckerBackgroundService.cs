
using FileSyncDemo.BusinessLogic.Services.Interfaces.LogInterfaces;
using FileSyncDemo.Core.Entities;
using FileSyncDemo.Core.Persistance;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FileSyncDemo.BusinessLogic.BackgroundServices
{
    // This checks every 30s if all files from database exist on the path, else del them from db too
    // (maybe you del them manually from the path but they are still inside db)
    // 30s may be to long...?
    public class FileCheckerBackgroundService : BackgroundService
    {
        private Timer? _timer;
        private readonly IServiceProvider _serviceProvider;
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        public FileCheckerBackgroundService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(DoWork!, null, TimeSpan.Zero, TimeSpan.FromSeconds(30));
            return Task.CompletedTask;
        }

        //TODO: Add auto delete logs(create the models) and for the stop of this background service
        //      Inject log service
        private async void DoWork(object state)
        {
            await _semaphore.WaitAsync();
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var replicaRepository = scope.ServiceProvider.GetRequiredService<IRepository<ReplicaFile>>();
                    var sourceRepository = scope.ServiceProvider.GetRequiredService<IRepository<SourceFile>>();

                    //I Have just one anyways, but for future implementation I used GetAll instead of FirstOrDefault
                    var allSources = await sourceRepository.GetAllAsync();
                    foreach (var source in allSources)
                    {
                        if (!File.Exists(source.FilePath))
                        {
                            await sourceRepository.DeleteAsync(source);
                            await sourceRepository.SaveChangesAsync();

                            var message = $"SourceFile {source.FileName} auto deleted";
                            var generateSyncLog = scope.ServiceProvider.GetRequiredService<ILogForDeleteService>();
                            await generateSyncLog.LogAutoDeleteSuccessAsync(message);

                            //Based on their relationship from database, if I delete the Source, "orphans" should not exist..
                            var cascadeDeleteMessage = "All replica files deleted";
                            await generateSyncLog.LogAutoDeleteSuccessAsync(cascadeDeleteMessage);
                        }
                        //handle exception? + Fail Log + Create the model
                    }

                    var allReplicas = await replicaRepository.GetAllAsync();
                    foreach (var replica in allReplicas)
                    {
                        if (!File.Exists(replica.FilePath))
                        {
                            await replicaRepository.DeleteAsync(replica);
                            await replicaRepository.SaveChangesAsync();
                            var message = $"ReplicaFile {replica.FileName} auto deleted";
                            var generateSyncLog = scope.ServiceProvider.GetRequiredService<ILogForDeleteService>();
                            await generateSyncLog.LogAutoDeleteSuccessAsync(message);
                        }
                        //handle exception? + Fail Log + Create the model
                    }
                }
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, Timeout.Infinite);
            //maybe log here ?
            await base.StopAsync(cancellationToken);
        }

        public override void Dispose()
        {
            _timer?.Dispose();
            base.Dispose();
        }
    }
}



/*
using FileSyncDemo.Core.Entities;
using FileSyncDemo.Core.Persistance;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FileSyncDemo.BusinessLogic.BackgroundServices
{
   // This checks if all files from database exist on the path, else del them from db too
   // (maybe you del them manually but they are still inside db)
   // I haven t tested this background service yet, so this might not work well at first.
    public class FileCheckerBackgroundService : BackgroundService
    {
        private Timer? _timer;
        private readonly IRepository<ReplicaFile> _replicaRepository;
        private readonly IRepository<SourceFile> _sourceRepository;
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1,1);

        public FileCheckerBackgroundService
        (
            IRepository<ReplicaFile> replicaRepository,
            IRepository<SourceFile> sourceRepository
        )
        {
            _replicaRepository = replicaRepository;
            _sourceRepository = sourceRepository;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(DoWork!, null, TimeSpan.Zero, TimeSpan.FromSeconds(30));
            return Task.CompletedTask;
        }

        //TODO: Add auto delete logs(create the models) and for the stop of this background service
        //      Inject log service
        private async void DoWork(object state)
        {
            await _semaphore.WaitAsync();
            try{

                //I Have just one anyways, but for future implementation I used GetAll instead of FirstOrDefault
                var allSources = await _sourceRepository.GetAllAsync();              
                foreach (var source in allSources)
                {
                    if (!File.Exists(source.FilePath))
                    {
                        await _sourceRepository.DeleteAsync(source);
                        await _sourceRepository.SaveChangesAsync();
                        //log
                    }
                    //handle exception?
                }

                var allReplicas = await _replicaRepository.GetAllAsync();
                foreach (var replica in allReplicas)
                {
                    if (!File.Exists(replica.FilePath))
                    {
                        await _replicaRepository.DeleteAsync(replica);
                        await _replicaRepository.SaveChangesAsync();
                        //log
                    }
                    //handle exception?
                }


            }
            finally
            {
                _semaphore.Release();
            }

        }


        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, Timeout.Infinite);
            //log
            await base.StopAsync(cancellationToken);
        }

        public override void Dispose()
        {
            _timer?.Dispose();
            base.Dispose();
        }
    }
}
*/