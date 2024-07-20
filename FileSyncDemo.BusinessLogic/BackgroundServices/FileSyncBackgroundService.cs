using FileSyncDemo.BusinessLogic.Services.Interfaces;
using FileSyncDemo.BusinessLogic.Services.Interfaces.LogInterfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FileSyncDemo.BusinessLogic.BackgroundServices
{
    // This is the background sync with can be started or stopped manually (so you have to enable it first)
    // First, you start without any SourceFiles or ReplicaFiles and you don't have what to sync, that's why.
    public class FileSyncBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private Timer? _timer;
        private bool _isManualStart = false;
        private readonly object _lock = new object();

        public FileSyncBackgroundService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var syncService = scope.ServiceProvider.GetRequiredService<ISyncService>();
                syncService.SynchronizeFileAutomaticallyAsync().Wait();
            }
        }

        public void StartManualAutoSync()
        {
            lock (_lock)
            {
                if (_isManualStart)
                {
                    //TODO: Log here ??
                    Console.WriteLine("Start already in progress");
                    return;
                }


                _timer = new Timer(DoWork!, null, TimeSpan.Zero, TimeSpan.FromSeconds(30));
                _isManualStart = true;

                Task.Run(async () =>
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var generateSyncLog = scope.ServiceProvider.GetRequiredService<ILogForSyncService>();
                        await generateSyncLog.LogManualAutoSyncStartAsync();
                    }
                }).Wait();

            }

        }

        public void StopManualAutoSync()
        {
            lock(_lock)
            {

                if (!_isManualStart)
                {
                    //TODO: Log here ??
                    Console.WriteLine("No Service is started for you to stop it!");
                    return;
                }


                _timer?.Change(Timeout.Infinite, Timeout.Infinite);
                _isManualStart = false;

                Task.Run(async () =>
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var generateSyncLog = scope.ServiceProvider.GetRequiredService<ILogForSyncService>();
                        await generateSyncLog.LogManualAutoSyncStopAsync();
                    }
                }).Wait();


            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            
            _timer?.Change(Timeout.Infinite, Timeout.Infinite);
            _isManualStart = false;

            using (var scope = _serviceProvider.CreateScope())
            {
                var generateSyncLog = scope.ServiceProvider.GetRequiredService<ILogForSyncService>();
                await generateSyncLog.LogAutomaticSyncStopAsync();
            }

                await base.StopAsync(cancellationToken);
            
        }

        public override void Dispose()
        {
            _timer?.Dispose();
            base.Dispose();
        }
    }
}

