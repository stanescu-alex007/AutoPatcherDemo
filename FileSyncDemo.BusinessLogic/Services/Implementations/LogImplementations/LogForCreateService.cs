using FileSyncDemo.BusinessLogic.Models.ActivityLogModels.LogCreateModels.Manually.Failed;
using FileSyncDemo.BusinessLogic.Models.ActivityLogModels.LogCreateModels.Manually.Success;
using FileSyncDemo.BusinessLogic.Services.Interfaces;
using FileSyncDemo.BusinessLogic.Services.Interfaces.LogInterfaces;

namespace FileSyncDemo.BusinessLogic.Services.Implementations.LogImplementations;

public class LogForCreateService : ILogForCreateService
{
    private readonly IActivityLogService _activityLogService;

    public LogForCreateService(IActivityLogService activityLogService)
    {
        _activityLogService = activityLogService;
    }
    public async Task LogManualCreateFailureAsync(string errorMessage)
    {
        var logCreateDTO = new ManualFailedCreateLogDTO
        {
            Timestamp = DateTime.Now,
            Message = errorMessage

        };
        await _activityLogService.LogAsync(logCreateDTO);

        Console.WriteLine("----------------------------------------------------------------------");
        Console.WriteLine($"An error occurred during manual creation: {errorMessage}");
        Console.WriteLine("----------------------------------------------------------------------");
    }

    public async Task LogManualCreateSuccessAsync(string successMessage)
    {
        var logCreateDTO = new ManualSuccessCreateLogDTO
        {
            Timestamp = DateTime.Now,
            Message = successMessage

        };
        await _activityLogService.LogAsync(logCreateDTO);

        Console.WriteLine("----------------------------------------------------------------------");
        Console.WriteLine($"File manually created successfully at {DateTime.Now}");
        Console.WriteLine("----------------------------------------------------------------------");
    }
}
