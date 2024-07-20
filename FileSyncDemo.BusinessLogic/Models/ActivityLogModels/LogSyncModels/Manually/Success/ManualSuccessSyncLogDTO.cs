using FileSyncDemo.BusinessLogic.Models.ActivityLogModels.LogSyncModels.Manually;

namespace FileSyncDemo.BusinessLogic.Models.ActivityLogModels;

public class ManualSuccessSyncLogDTO : ManualSyncLog
{
    public string? Message { get; set; }
    public string Status { get;} = "Success";
}
