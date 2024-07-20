using FileSyncDemo.BusinessLogic.Models.ActivityLogModels;

namespace FileSyncDemo.BusinessLogic.Services.Interfaces;

public interface IActivityLogService
{
    Task LogAsync<T>(T logModel) where T : LogModel;
}
