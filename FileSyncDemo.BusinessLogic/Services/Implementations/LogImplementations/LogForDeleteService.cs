using FileSyncDemo.BusinessLogic.Models.ActivityLogModels.LogCreateModels.Manually.Success;
using FileSyncDemo.BusinessLogic.Models.ActivityLogModels.LogDeleteModels.Automatically.Success;
using FileSyncDemo.BusinessLogic.Models.ActivityLogModels.LogDeleteModels.Manually.Failed;
using FileSyncDemo.BusinessLogic.Models.ActivityLogModels.LogDeleteModels.Manually.Success;
using FileSyncDemo.BusinessLogic.Services.Interfaces;
using FileSyncDemo.BusinessLogic.Services.Interfaces.LogInterfaces;

namespace FileSyncDemo.BusinessLogic.Services.Implementations.LogImplementations;

public class LogForDeleteService : ILogForDeleteService
{
    private readonly IActivityLogService _activityLogService;

    public LogForDeleteService(IActivityLogService activityLogService)
    {
        _activityLogService = activityLogService;
    }

    public async Task LogAutoDeleteSuccessAsync(string message)
    {
        var logDeleteDTO = new AutomaticSuccessDeleteLogDTO
        {
            Timestamp = DateTime.Now,
            Message = message

        };
        await _activityLogService.LogAsync(logDeleteDTO);

        Console.WriteLine("----------------------------------------------------------------------");
        Console.WriteLine($"File was auto deleted: {message}");
        Console.WriteLine("----------------------------------------------------------------------");
    }

    public async Task LogManualDeleteFailureAsync(string errorMessage)
    {
        var logDeleteDTO = new ManualFailedDeleteLogDTO
        {
            Timestamp = DateTime.Now,
            Message = errorMessage

        };
        await _activityLogService.LogAsync(logDeleteDTO);

        Console.WriteLine("----------------------------------------------------------------------");
        Console.WriteLine($"An error occurred while trying to delete: {errorMessage}");
        Console.WriteLine("----------------------------------------------------------------------");
    }

    public async Task LogManualDeleteSuccessAsync(string successMessage)
    {
        var logDeleteDTO = new ManualSuccessDeleteLogDTO
        {
            Timestamp = DateTime.Now,
            Message = successMessage

        };
        await _activityLogService.LogAsync(logDeleteDTO);

        Console.WriteLine("----------------------------------------------------------------------");
        Console.WriteLine($"File was deleted: {successMessage}");
        Console.WriteLine("----------------------------------------------------------------------");
    }
}
