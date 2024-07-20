using FileSyncDemo.BusinessLogic.Models.ActivityLogModels.LogSyncModels.Manually;

namespace FileSyncDemo.BusinessLogic.Models.ActivityLogModels;

public class ManualFailedSyncLogDTO : ManualSyncLog
{
    public string? Message { get; set; }
    public string Status { get;} = "Fail";
}
