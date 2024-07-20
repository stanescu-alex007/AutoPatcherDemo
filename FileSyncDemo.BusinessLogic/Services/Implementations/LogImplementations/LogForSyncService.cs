using FileSyncDemo.BusinessLogic.Models.ActivityLogModels.LogSyncModels.Automatically.Failed;
using FileSyncDemo.BusinessLogic.Models.ActivityLogModels.LogSyncModels.Automatically.Started;
using FileSyncDemo.BusinessLogic.Models.ActivityLogModels.LogSyncModels.Automatically.Stopped;
using FileSyncDemo.BusinessLogic.Models.ActivityLogModels.LogSyncModels.Automatically.Success;
using FileSyncDemo.BusinessLogic.Models.ActivityLogModels;
using FileSyncDemo.BusinessLogic.Services.Interfaces;
using FileSyncDemo.BusinessLogic.Services.Interfaces.LogInterfaces;
using FileSyncDemo.BusinessLogic.Models.ActivityLogModels.LogSyncModels.Manually.StartAuto;
using FileSyncDemo.BusinessLogic.Models.ActivityLogModels.LogSyncModels.Manually.StopAuto;

namespace FileSyncDemo.BusinessLogic.Services.Implementations.LogImplementations;

public class LogForSyncService : ILogForSyncService
{
    private readonly IActivityLogService _activityLogService;

    public LogForSyncService(IActivityLogService activityLogService)
    {
        _activityLogService = activityLogService;
    }

    public async Task LogAutomaticSyncFailureAsync(string failureMessage)
    {
        var logFailDTO = new AutomaticFailedSyncLogDTO
        {
            Timestamp = DateTime.Now,
            Message = failureMessage
        };

        await _activityLogService.LogAsync(logFailDTO);

        Console.WriteLine("----------------------------------------------------------------------");
        Console.WriteLine($"An error occurred during automatic synchronization: {failureMessage}");
        Console.WriteLine("----------------------------------------------------------------------");
    }

    public async Task LogAutomaticSyncSuccessAsync(string successMessage)
    {
        var logSuccessDTO = new AutomaticSuccessSyncLogDTO
        {
            Timestamp = DateTime.Now,
            Message = successMessage
        };
        await _activityLogService.LogAsync(logSuccessDTO);

        Console.WriteLine("----------------------------------------------------------------------");
        Console.WriteLine($"Files automatically synchronized successfully at {DateTime.Now}");
        Console.WriteLine("----------------------------------------------------------------------");
    }

    public async Task LogManualSyncFailureAsync(string failureMessage)
    {
        var logManualFailDTO = new ManualFailedSyncLogDTO
        {
            Timestamp = DateTime.Now,
            Message = failureMessage
        };
        await _activityLogService.LogAsync(logManualFailDTO);

        Console.WriteLine("----------------------------------------------------------------------");
        Console.WriteLine($"An error occurred during manual synchronization: {failureMessage}");
        Console.WriteLine("----------------------------------------------------------------------");
    }

    public async Task LogManualSyncSuccessAsync(string successMessage)
    {
        var logManualSuccesDTO = new ManualSuccessSyncLogDTO
        {
            Timestamp = DateTime.Now,
            Message = successMessage
            
        };
        await _activityLogService.LogAsync(logManualSuccesDTO);

        Console.WriteLine("----------------------------------------------------------------------");
        Console.WriteLine($"Files manually synchronized successfully at {DateTime.Now}");
        Console.WriteLine("----------------------------------------------------------------------");
    }
    public async Task LogAutomaticSyncStartAsync()
    {

        var logStartDTO = new AutomaticStartSyncLogDTO
        {
            Timestamp = DateTime.Now
        };

        await _activityLogService.LogAsync(logStartDTO);

        Console.WriteLine("----------------------------------------------------------------------");
        Console.WriteLine($"Automatic synchronization started at {DateTime.Now}");
        Console.WriteLine("----------------------------------------------------------------------");
    }

    public async Task LogAutomaticSyncStopAsync()
    {
        var logStopDTO = new AutomaticStopSyncLogDTO
        {
            Timestamp = DateTime.Now
        };

        await _activityLogService.LogAsync(logStopDTO);

        Console.WriteLine("----------------------------------------------------------------------");
        Console.WriteLine($"Automatic synchronization stopped at {DateTime.Now}");
        Console.WriteLine("----------------------------------------------------------------------");
    }

    public async Task LogManualAutoSyncStartAsync()
    {
        var logStopDTO = new ManualAutoStartSyncLogDTO
        {
            Timestamp = DateTime.Now
        };

        await _activityLogService.LogAsync(logStopDTO);

        Console.WriteLine("----------------------------------------------------------------------");
        Console.WriteLine($"Manual Auto synchronization started at {DateTime.Now}");
        Console.WriteLine("----------------------------------------------------------------------");
    }

    public async Task LogManualAutoSyncStopAsync()
    {
        var logStopDTO = new ManualAutoStopSyncLogDTO
        {
            Timestamp = DateTime.Now
        };

        await _activityLogService.LogAsync(logStopDTO);

        Console.WriteLine("----------------------------------------------------------------------");
        Console.WriteLine($"Manual Auto synchronization stopped at {DateTime.Now}");
        Console.WriteLine("----------------------------------------------------------------------");
    }
}
