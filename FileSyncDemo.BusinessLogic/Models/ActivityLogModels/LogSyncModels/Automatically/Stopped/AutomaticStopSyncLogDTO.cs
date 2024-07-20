namespace FileSyncDemo.BusinessLogic.Models.ActivityLogModels.LogSyncModels.Automatically.Stopped;

public class AutomaticStopSyncLogDTO : AutomaticSyncLog
{
    public string Message { get;} = "Automatic Sync Service Stopped";
    public string Status { get;} = "Success";
}
