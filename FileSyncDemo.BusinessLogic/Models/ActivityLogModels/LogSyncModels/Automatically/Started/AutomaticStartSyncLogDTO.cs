namespace FileSyncDemo.BusinessLogic.Models.ActivityLogModels.LogSyncModels.Automatically.Started;

public class AutomaticStartSyncLogDTO : AutomaticSyncLog
{
    public string Message { get;} = "Automatic Sync Started";
    public string Status { get;} = "Success";
}
